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
    public class UserService:IUserService
    {
        private ESSDbContext _context;

        public UserService(ESSDbContext context)
        {
            _context = context;
        }
        public Result<User> Save(User Entity)
        {
             var result = new Result<User>();
             try
             {
                 var objtosave = _context.Users.FirstOrDefault(u => u.UserId == Entity.UserId);
                 if (objtosave == null)
                 {
                     objtosave = new User();
                    _context.Users.Add(Entity);
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
                var objtodelete = _context.Users.FirstOrDefault(q => q.UserId == Id);
                if (objtodelete == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid Data";
                    return false;
                }

                _context.Users.Remove(objtodelete);
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

        public Result<List<User>> GetAll(string key = "")
        {
            var result = new Result<List<User>>() { Data = new List<User>() };
            try
            {
                IQueryable<User> query = _context.Users;
                if (ValidationHelper.IsIntValid(key))
                {
                    query = query.Where(q => q.UserId.Equals(Int32.Parse(key)));
                }
                if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.UserId.Equals(Int32.Parse(key)));
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

        public Result<User> GetById(int Id)
        {
            var result = new Result<User>();
            try
            {
                var obj = _context.Users.FirstOrDefault(u => u.UserId == Id);
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

        public bool IsValid(User obj, Result<User> result)
        {
            if (!ValidationHelper.IsStringValid((obj.UserId.ToString())))
            {
                result.HasError = true;
                result.Message = "Invalid UserId";
                return false;
            }

            return true;
        }

        public Result<int> GetLastId(string email)
        {
            var result = new Result<int>();
            try
            {
                var lastobj = _context.Users.FirstOrDefault(u => u.Email == email); ;
                if (lastobj == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid Data";
                    return result;
                }

                result.Data = lastobj.UserId;

            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;
                return result;
            }

            return result;
        }
    }
}
