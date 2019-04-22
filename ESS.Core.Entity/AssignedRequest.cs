using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESS.Core.Entity
{
    public class AssignedRequest
    {
        [Required]
        [Key]
        public int AssignedId { get; set; }

        [ForeignKey("RequestForService")]
        public int RequestId { get; set; }

        public RequestForService RequestForService { get; set; }

        public int EmployeeId { get; set; }

        public int VolunteerId { get; set; }

        public int VolunteerDoctorId { get; set; }



        

        public AssignedRequest()
        {
            VolunteerId = 0;
            VolunteerDoctorId = 0;
        }


    }
}
