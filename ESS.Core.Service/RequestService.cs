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
    public class RequestService:IRequestService
    {
        private ESSDbContext _context;

        public RequestService(ESSDbContext context)
        {
            _context = context;
        }
        public Result<RequestForService> Save(RequestForService Entity)
        {
            var result = new Result<RequestForService>();
            try
            {
                var objtosave = _context.Requests.FirstOrDefault(u => u.RequestId == Entity.RequestId);
                if (objtosave == null)
                {
                    objtosave = new RequestForService();
                    _context.Requests.Add(Entity);
                }
                else
                {
                    objtosave = new RequestForService();
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
                var objtodelete = _context.Requests.FirstOrDefault(q => q.RequestId == Id);
                if (objtodelete == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid Data";
                    return false;
                }

                _context.Requests.Remove(objtodelete);
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

        public Result<List<RequestForService>> GetAll(string key = "")
        {
            var result = new Result<List<RequestForService>>() { Data = new List<RequestForService>() };
            try
            {
                
                IQueryable<RequestForService> query = _context.Requests;
                if (ValidationHelper.IsIntValid(key))
                {
                    query = query.Where(q => q.RequestId.Equals(Int32.Parse(key)));
                }
                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.RequestId.Equals(Int32.Parse(key)));
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

        public Result<RequestForService> GetById(int Id)
        {
            var result = new Result<RequestForService>();
            try
            {
                var obj = _context.Requests.FirstOrDefault(u => u.RequestId == Id);
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

        public bool IsValid(RequestForService obj, Result<RequestForService> result)
        {
            if (!ValidationHelper.IsStringValid((obj.RequestId.ToString())))
            {
                result.HasError = true;
                result.Message = "Invalid RequestForServiceId";
                return false;
            }

            return true;
        }
    }
}
