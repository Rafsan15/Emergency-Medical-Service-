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
    public class RequestCountService:IRequestCount
    {
        private ESSDbContext _context;

        public RequestCountService(ESSDbContext context)
        {
            _context = context;
        }

        public Result<RequestCount> Save(RequestCount Entity)
        {
            var result = new Result<RequestCount>();
            try
            {
                var objtosave = _context.RequestCounts.FirstOrDefault(u => u.CountId == Entity.CountId);
                if (objtosave == null)
                {
                    objtosave = new RequestCount();
                    _context.RequestCounts.Add(Entity);
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
                var objtodelete = _context.RequestCounts.FirstOrDefault(q => q.CountId == Id);
                if (objtodelete == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid Data";
                    return false;
                }

                _context.RequestCounts.Remove(objtodelete);
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

        public Result<List<RequestCount>> GetAll(string key = "")
        {
            var result = new Result<List<RequestCount>>() { Data = new List<RequestCount>() };
            try
            {
                IQueryable<RequestCount> query = _context.RequestCounts;
                if (ValidationHelper.IsIntValid(key))
                {
                    query = query.Where(q => q.CountId.Equals(Int32.Parse(key)));
                }
                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.CountId.Equals(Int32.Parse(key)));
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

        public Result<RequestCount> GetById(int Id)
        {
            var result = new Result<RequestCount>();
            try
            {
                var obj = _context.RequestCounts.FirstOrDefault(u => u.CountId == Id);
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

        public bool IsValid(RequestCount obj, Result<RequestCount> result)
        {
            if (!ValidationHelper.IsStringValid((obj.CountId.ToString())))
            {
                result.HasError = true;
                result.Message = "Invalid RequestCountId";
                return false;
            }

            return true;
        }
    }
}
