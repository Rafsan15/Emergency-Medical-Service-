using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESS.Core.Entity
{
    public class Volunteer
    {
        [Key]
        public int VolunteerId { get; set; }

        public string IsActive { get; set; }

        public string IsApprove { get; set; }

        public string JobDetails { get; set; }

        public string CurrentLocation { get; set; }

        public string Area { get; set; }
     
        public int UserId { get; set; }

        public string IsVolunteerDone { get; set; }

       // public string WorkShopStatus { get; set; }


        public Volunteer()
        {
            IsActive = "false";
            IsApprove = "false";
            CurrentLocation = "Bashundhara";
            Area = "Bashundhara";
            IsVolunteerDone = "Not Done";
         //   WorkShopStatus = "false";

        }

    }
}
