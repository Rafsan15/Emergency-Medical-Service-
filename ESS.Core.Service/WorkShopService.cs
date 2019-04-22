using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESS.Core.Entity;
using ESS.Infrastructure;
using ESS.Service.Interface;
using Framework;

namespace ESS.Core.Service
{
    public class WorkShopService:IWorkShopServiceInterface
    {
        private ESSDbContext _context;

        public WorkShopService(ESSDbContext context)
        {
            _context = context;
        }
        public Result<WorkShop> Save(WorkShop Entity)
        {
            var result = new Result<WorkShop>();
            try
            {
                var objtosave = _context.WorkShops.FirstOrDefault(u => u.WorkShopId == Entity.WorkShopId);
                if (objtosave == null)
                {
                    objtosave = new WorkShop();
                    _context.WorkShops.Add(Entity);
                }
                else
                {
                    objtosave = Entity;

                }

                if (!IsValid(objtosave, result))
                {
                    return result;
                }


                _context.SaveChanges();

            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;

            }

            return result;


        }

        public bool Delete(int Id)
        {
            var result = new Result<bool>();
            try
            {
                var objtodelete = _context.WorkShops.FirstOrDefault(q => q.WorkShopId == Id);
                if (objtodelete == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid Data";
                    return false;
                }

                _context.WorkShops.Remove(objtodelete);
                _context.SaveChanges();

            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;
                return false;
            }

            return true;
        }

        public Result<List<WorkShop>> GetAll(string key = "")
        {
            var result = new Result<List<WorkShop>>() { Data = new List<WorkShop>() };
            try
            {
                IQueryable<WorkShop> query = _context.WorkShops;
                if (ValidationHelper.IsIntValid(key))
                {
                    query = query.Where(q => q.WorkShopId.Equals(Int32.Parse(key)));
                }
                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.WorkShopId.Equals(Int32.Parse(key)));
                }

                result.Data = query.ToList();
            }
            catch (Exception e)
            {

                result.HasError = true;
                result.Message = e.Message;
            }

            return result;
        }

        public Result<WorkShop> GetById(int Id)
        {
            var result = new Result<WorkShop>();
            try
            {
                var obj = _context.WorkShops.FirstOrDefault(u => u.WorkShopId == Id);
                if (obj == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid Data";
                    return result;
                }

                result.Data = obj;
            }
            catch (Exception e)
            {

                result.HasError = true;
                result.Message = e.Message;
            }

            return result;
        }

        public bool IsValid(WorkShop obj, Result<WorkShop> result)
        {
            if (!ValidationHelper.IsStringValid((obj.WorkShopId.ToString())))
            {
                result.HasError = true;
                result.Message = "Invalid WorkShopId";
                return false;
            }

            return true;
        }

       
    }
}
