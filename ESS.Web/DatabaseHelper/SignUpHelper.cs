using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ESS.Core.Entity;
using ESS.Web.ViewModel;

namespace ESS.Web.DatabaseHelper
{
    public class SignUpHelper
    {
        public User GetUserobj(SignUpViewModel signUp)
        {
            User user = new User
            {
                Address = signUp.Address,
               // DOB = signUp.DOB,
                Email = signUp.Email,
                Name = signUp.Name,
                Phone = signUp.Phone,
                //Photo = signUp.Photo,
                UserType = signUp.UserType,
                Password = signUp.Password
            };
            if (signUp.Photo==null)
                user.Photo = "Not Interested";
            return user;
        }

        public Employee GetEmployeeobj(SignUpViewModel signUp)
        {
            Employee employee = new Employee { UserId = signUp.UserId, Designation = signUp.Designation };

            return employee;
        }

        public Volunteer GetVolunteerobj(SignUpViewModel signUp)
        {
            Volunteer volunteer = new Volunteer { UserId = signUp.UserId, JobDetails = signUp.JobDetails };

            return volunteer;
        }

        public VolunteerDoctor GetVolunteerDoctorobj(SignUpViewModel signUp)
        {
            VolunteerDoctor volunteerDoctor = new VolunteerDoctor
            {
                UserId = signUp.UserId,
                HospitalName = signUp.HospitalName,
                SpecialDomain = signUp.SpecialDomain
            };
            if (signUp.HospitalName==null)
                volunteerDoctor.HospitalName = "Not Mention Yet";


            return volunteerDoctor;
        }
    }
}