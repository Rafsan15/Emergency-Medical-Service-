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
  public  class VolunteerDoctorService:IVolunteerDoctorService
    {
        private ESSDbContext _context;

        public VolunteerDoctorService(ESSDbContext context)
        {
            _context = context;
        }
        public Result<VolunteerDoctor> Save(VolunteerDoctor Entity)
        {
            var result = new Result<VolunteerDoctor>();
            try
            {
                var objtosave = _context.VolunteerDoctors.FirstOrDefault(u => u.DoctorId == Entity.DoctorId);
                if (objtosave == null)
                {
                    objtosave = new VolunteerDoctor();
                    _context.VolunteerDoctors.Add(Entity);
                }
                else
                {
                    objtosave = new VolunteerDoctor();
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
                var objtodelete = _context.VolunteerDoctors.FirstOrDefault(q => q.UserId == Id);
                if (objtodelete == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid Data";
                    return false;
                }

                _context.VolunteerDoctors.Remove(objtodelete);
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

        public Result<List<VolunteerDoctor>> GetAll(string key = "")
        {
            var result = new Result<List<VolunteerDoctor>>() { Data = new List<VolunteerDoctor>() };
            try
            {
                IQueryable<VolunteerDoctor> query = _context.VolunteerDoctors;
                if (ValidationHelper.IsIntValid(key))
                {
                    query = query.Where(q => q.DoctorId.Equals(Int32.Parse(key)));
                }
                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.DoctorId.Equals(Int32.Parse(key)));
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

        public Result<VolunteerDoctor> GetById(int Id)
        {
            var result = new Result<VolunteerDoctor>();
            try
            {
                var obj = _context.VolunteerDoctors.FirstOrDefault(u => u.UserId == Id);
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

        public Result<VolunteerDoctor> GetVolunteerDoctorById(int Id)
        {
            var result = new Result<VolunteerDoctor>();
            try
            {
                var obj = _context.VolunteerDoctors.FirstOrDefault(u => u.DoctorId == Id);
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


        public bool IsValid(VolunteerDoctor obj, Result<VolunteerDoctor> result)
        {
            if (!ValidationHelper.IsStringValid((obj.DoctorId.ToString())))
            {
                result.HasError = true;
                result.Message = "Invalid VolunteerDoctorId";
                return false;
            }

            return true;
        }
    }
}
