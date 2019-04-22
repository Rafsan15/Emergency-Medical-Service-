using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using ESS.Core.Entity;
using ESS.Infrastructure;
using ESS.Service.Interface;
using Framework;

namespace ESS.Core.Service
{
   public class AssignedRequestService:IAssignedRequestService
    {
        private ESSDbContext _context;

        public AssignedRequestService(ESSDbContext context)
        {
            _context = context;
        }
        public Result<AssignedRequest> Save(AssignedRequest Entity)
        {
            var result = new Result<AssignedRequest>();
            try
            {
                var objtosave = _context.Assigned.FirstOrDefault(u => u.AssignedId == Entity.AssignedId);
                if (objtosave == null)
                {
                    objtosave = new AssignedRequest();
                    _context.Assigned.Add(Entity);
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
                var objtodelete = _context.Assigned.FirstOrDefault(q => q.AssignedId == Id);
                if (objtodelete == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid Data";
                    return false;
                }

                _context.Assigned.Remove(objtodelete);
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

        public Result<List<AssignedRequest>> GetAll(string key = "")
        {
            var result = new Result<List<AssignedRequest>>() { Data = new List<AssignedRequest>() };
            try
            {
                int key2=Int32.Parse(key);
                IQueryable<AssignedRequest> query = _context.Assigned;
                if (ValidationHelper.IsIntValid(key))
                {
                    query = query.Where(q => q.RequestId == key2);
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

        public Result<List<AssignedRequest>> GetVoluenteerAll(int key = 0)
        {
            var result = new Result<List<AssignedRequest>>() { Data = new List<AssignedRequest>() };
            try
            {
                IQueryable<AssignedRequest> query = _context.Assigned;
                if (ValidationHelper.IsIntValid(key+""))
                {
                    query = query.Where(q => q.VolunteerId==key );
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

        public Result<List<AssignedRequest>> GetDoctorAll(int key = 0)
        {
            var result = new Result<List<AssignedRequest>>() { Data = new List<AssignedRequest>() };
            try
            {
                IQueryable<AssignedRequest> query = _context.Assigned;
                if (ValidationHelper.IsIntValid(key + ""))
                {
                    query = query.Where(q => q.VolunteerDoctorId == key);
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

        public Result<List<AssignedRequest>> GetEmployeeAll(int key = 0)
        {
            var result = new Result<List<AssignedRequest>>() { Data = new List<AssignedRequest>() };
            try
            {
                IQueryable<AssignedRequest> query = _context.Assigned;
                if (ValidationHelper.IsIntValid(key + ""))
                {
                    query = query.Where(q => q.EmployeeId == key);
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

        public Result<AssignedRequest> GetById(int Id)
        {
            var result = new Result<AssignedRequest>();
            try
            {
                var obj = _context.Assigned.FirstOrDefault(u => u.RequestId == Id);
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

        public bool IsValid(AssignedRequest obj, Result<AssignedRequest> result)
        {
            if (!ValidationHelper.IsStringValid((obj.AssignedId.ToString())))
            {
                result.HasError = true;
                result.Message = "Invalid AssignedRequestId";
                return false;
            }

            return true;
        }
    }
}
