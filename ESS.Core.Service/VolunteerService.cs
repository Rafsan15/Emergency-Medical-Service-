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
    public class VolunteerService: IVolunteerService
    {
        private ESSDbContext _context;

        public VolunteerService(ESSDbContext context)
        {
            _context = context;
        }
        public Result<Volunteer> Save(Volunteer Entity)
        {
            var result = new Result<Volunteer>();
            try
            {
                var objtosave = _context.Volunteers.FirstOrDefault(u => u.VolunteerId == Entity.VolunteerId);
                if (objtosave == null)
                {
                    objtosave = new Volunteer();
                    _context.Volunteers.Add(Entity);
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
                var objtodelete = _context.Volunteers.FirstOrDefault(q => q.UserId == Id);
                if (objtodelete == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid Data";
                    return false;
                }

                _context.Volunteers.Remove(objtodelete);
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

        public Result<List<Volunteer>> GetAll(string key = "")
        {
            var result = new Result<List<Volunteer>>() { Data = new List<Volunteer>() };
            try
            {
                IQueryable<Volunteer> query = _context.Volunteers;
                if (ValidationHelper.IsIntValid(key))
                {
                    query = query.Where(q => q.VolunteerId.Equals(Int32.Parse(key)));
                }
                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.VolunteerId.Equals(Int32.Parse(key)));
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

        public Result<Volunteer> GetById(int Id)
        {
            var result = new Result<Volunteer>();
            try
            {
                var obj = _context.Volunteers.FirstOrDefault(u => u.UserId == Id);
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

        public Result<Volunteer> GetVolunteerById(int Id)
        {
            var result = new Result<Volunteer>();
            try
            {
                var obj = _context.Volunteers.FirstOrDefault(u => u.VolunteerId == Id);
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

        public bool IsValid(Volunteer obj, Result<Volunteer> result)
        {
            if (!ValidationHelper.IsStringValid((obj.VolunteerId.ToString())))
            {
                result.HasError = true;
                result.Message = "Invalid VolunteerId";
                return false;
            }

            return true;
        }

    }
}
