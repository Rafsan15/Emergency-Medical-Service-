using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ESS.Core.Entity;

namespace ESS.Web.ViewModel
{
    public class SignUpViewModel
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Gender { get; set; }

        public string Photo { get; set; }

        public string UserType { get; set; }

        public DateTime DOB { get; set; }

        public string Designation { get; set; }

        public string JobDetails { get; set; }

        public string SpecialDomain { get; set; }

        public string HospitalName { get; set; }

    }
}