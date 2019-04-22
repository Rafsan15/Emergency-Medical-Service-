using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESS.Core.Entity
{
    public class VolunteerDoctor
    {
        [Required]
        [Key]
        public int DoctorId { get; set; }

        public string SpecialDomain { get; set; }

        public string HospitalName { get; set; }

        public string IsActive { get; set; }

        public string CurrentLocation { get; set; }

        public string Area { get; set; }

        public string IsDoctorDone { get; set; }

     //   public string WorkShopStatus { get; set; }


        public int UserId { get; set; }

        public VolunteerDoctor()
        {
       //     WorkShopStatus = "false";
            IsActive = "false";
            CurrentLocation = "Bashundhara";
            Area = "Bashundhara";
            IsDoctorDone = "Not Done";

        }
    }
}
