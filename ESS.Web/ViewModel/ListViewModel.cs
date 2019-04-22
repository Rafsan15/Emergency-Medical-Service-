using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ESS.Core.Entity;

namespace ESS.Web.ViewModel
{
    public class ListViewModel : IEnumerable
    {
        public List<Volunteer> Volunteers =new List<Volunteer>();
        public List<User> Users =new List<User>();
        public List<Employee> Employees =new List<Employee>();
        public List<VolunteerDoctor> VolunteerDoctors =new List<VolunteerDoctor>();
        public List<UserDoctorViewModel> UserDoctorViewModel = new List<UserDoctorViewModel>();
        public List<UserVolunteerViewModel> UserVolunteerViewModels = new List<UserVolunteerViewModel>();
        public RequestForService Request { get; set; }
        public WorkShop WorkShop { get; set; }


        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}