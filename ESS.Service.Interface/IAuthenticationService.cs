using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESS.Core.Entity;
using Framework;

namespace ESS.Service.Interface
{
  public interface IAuthenticationService
    {
         bool Login(string email, string password);
         Result<User> GetByEmail(string email);
    }
}
