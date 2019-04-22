using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ESS.Core.Entity;
using Framework;

namespace ESS.Web.ViewModel
{
    public class UserVolunteerViewModel
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Gender { get; set; }

        public string JobDetails { get; set; }

        public string IsActive { get; set; }

        public string CurrentLocation { get; set; }

        public string Area { get; set; }


    }
}