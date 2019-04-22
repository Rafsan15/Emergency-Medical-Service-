using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ESS.Core.Entity;
using ESS.Infrastructure;
using ESS.Service.Interface;
using Framework;

namespace ESS.Core.Service
{
    public class AuthenticationService:IAuthenticationService
    {
        private ESSDbContext _context;

        public AuthenticationService(ESSDbContext context)
        {
            _context = context;
        }
        public bool Login(string email, string password)
        {
            try
            {
                var obj = _context.Users.FirstOrDefault(u => u.Email == email);
                if (obj.Password.Equals(password))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

           
        }

        public Result<User> GetByEmail(string email)
        {
            var result = new Result<User>();
            try
            {
                var obj = _context.Users.FirstOrDefault(u => u.Email.Equals(email));
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
    }
}
