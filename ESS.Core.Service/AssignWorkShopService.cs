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
   public class AssignWorkShopService:IAssignworkShopServiceinterface
    {
        private ESSDbContext _context;

        public AssignWorkShopService(ESSDbContext context)
        {
            _context = context;
        }
        public Result<AssignWorkShop> Save(AssignWorkShop Entity)
        {
            var result = new Result<AssignWorkShop>();
            try
            {
                var objtosave = _context.AssignWorkShops.FirstOrDefault(u => u.AssignWorkShopId == Entity.AssignWorkShopId);
                if (objtosave == null)
                {
                    objtosave = new AssignWorkShop();
                    _context.AssignWorkShops.Add(Entity);
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
                var objtodelete = _context.AssignWorkShops.FirstOrDefault(q => q.AssignWorkShopId == Id);
                if (objtodelete == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid Data";
                    return false;
                }

                _context.AssignWorkShops.Remove(objtodelete);
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

        public Result<List<AssignWorkShop>> GetAll(string key = "")
        {
            var result = new Result<List<AssignWorkShop>>() { Data = new List<AssignWorkShop>() };
            try
            {
                IQueryable<AssignWorkShop> query = _context.AssignWorkShops;
                if (ValidationHelper.IsIntValid(key))
                {
                    query = query.Where(q => q.AssignWorkShopId.Equals(Int32.Parse(key)));
                }
                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.AssignWorkShopId.Equals(Int32.Parse(key)));
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

        public Result<AssignWorkShop> GetById(int Id)
        {
            var result = new Result<AssignWorkShop>();
            try
            {
                var obj = _context.AssignWorkShops.FirstOrDefault(u => u.AssignWorkShopId == Id);
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

        public bool IsValid(AssignWorkShop obj, Result<AssignWorkShop> result)
        {
            if (!ValidationHelper.IsStringValid((obj.AssignWorkShopId.ToString())))
            {
                result.HasError = true;
                result.Message = "Invalid AssignWorkShopId";
                return false;
            }

            return true;
        }

        public Result<List<AssignWorkShop>> GetAllByWorkShopId(int key)
        {
            var result = new Result<List<AssignWorkShop>>() { Data = new List<AssignWorkShop>() };
            try
            {
                IQueryable<AssignWorkShop> query = _context.AssignWorkShops;
                
                    query = query.Where(q => q.WorkShopId.Equals(key));
               

                result.Data = query.ToList();
            }
            catch (Exception e)
            {

                result.HasError = true;
                result.Message = e.Message;
            }

            return result;
        }

        public Result<List<AssignWorkShop>> GetAllByUserId(int key)
        {
            var result = new Result<List<AssignWorkShop>>() { Data = new List<AssignWorkShop>() };
            try
            {
                IQueryable<AssignWorkShop> query = _context.AssignWorkShops;

                query = query.Where(q => q.UserId.Equals(key));


                result.Data = query.ToList();
            }
            catch (Exception e)
            {

                result.HasError = true;
                result.Message = e.Message;
            }

            return result;
        }
    }
}
