using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ESS.Core.Entity;

namespace ESS.Web.ViewModel
{
    public class DetailsViewModel
    {
        public SignUpViewModel SignUpViewModel(Employee emp, User user)
        {
            var signUp=new SignUpViewModel();
            signUp.UserId = user.UserId;
            signUp.Name = user.Name;
            signUp.Email = user.Email;
            signUp.Address = user.Address;
            signUp.Password = user.Password;
            signUp.Phone = user.Phone;
            signUp.DOB = user.DOB;
            signUp.Designation = emp.Designation;

            return signUp;
        }
        public SignUpViewModel SignUpViewModel(Volunteer vol, User user)
        {
            var signUp = new SignUpViewModel();
            signUp.UserId = user.UserId;
            signUp.Name = user.Name;
            signUp.Email = user.Email;
            signUp.Address = user.Address;
            signUp.Password = user.Password;
            signUp.Phone = user.Phone;
            signUp.DOB = user.DOB;
            signUp.JobDetails = vol.JobDetails;

            return signUp;
        }
        public SignUpViewModel SignUpViewModel(VolunteerDoctor voldoc, User user)
        {
            var signUp = new SignUpViewModel();
            signUp.UserId = user.UserId;
            signUp.Name = user.Name;
            signUp.Email = user.Email;
            signUp.Address = user.Address;
            signUp.Password = user.Password;
            signUp.Phone = user.Phone;
            signUp.DOB = user.DOB;
            signUp.SpecialDomain = voldoc.SpecialDomain;

            return signUp;
        }
    }
}