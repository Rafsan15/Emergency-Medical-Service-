using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ESS.Core.Entity;
using ESS.Web.ViewModel;

namespace ESS.Web.DatabaseHelper
{
    public class UpdateInstance
    {
        public User GetUpdatedUserObj(SignUpViewModel signUp, User user)
        {
            if (signUp.Name != null)
                user.Name = signUp.Name;

            if (signUp.Phone != null)
                user.Phone = signUp.Phone;

            if (signUp.Address != null)
                user.Address = signUp.Address;

            if (signUp.DOB != null)
                user.DOB = signUp.DOB;

            if (signUp.Email != null)
                user.Email = signUp.Email;

          

            if (signUp.Photo != null)
                user.Photo = signUp.Photo;

            if (signUp.Password != null)
                user.Password = signUp.Password;

            if (signUp.UserType != null)
                user.UserType = signUp.UserType;

            return user;
        }

        public Employee GetUpdatedEmployeeObj(SignUpViewModel signUp, Employee employee)
        {
            if (signUp.Designation != null)
                employee.Designation = signUp.Designation;

            return employee;
        }

        public Volunteer GetUpdatedVolunteerObj(SignUpViewModel signUp, Volunteer volunteer)
        {
            if (signUp.JobDetails != null)
                volunteer.JobDetails = signUp.JobDetails;

            return volunteer;
        }

        public VolunteerDoctor GetUpdatedVolunteerDoctorObj(SignUpViewModel signUp, VolunteerDoctor volunteerDoctor)
        {
            if (signUp.HospitalName != null)
                volunteerDoctor.HospitalName = signUp.HospitalName;

            if (signUp.SpecialDomain != null)
                volunteerDoctor.SpecialDomain = signUp.SpecialDomain;

            return volunteerDoctor;
        }
    }
}