using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using ESS.Core.Entity;

namespace App.Framework
{
    public static class HttpUtil
    {
       

        public static ESS.Core.Entity.User CurrentUser
        {
            get
            {
                try
                {
                    var user = JsonConvert.DeserializeObject<User>(HttpContext.Current.User.Identity.Name);
                    return user;
                }
                catch (Exception)
                {

                    return null;
                }
            }
        }

    }
}
