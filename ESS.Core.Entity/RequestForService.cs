using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESS.Core.Entity
{
    public class RequestForService
    {
        [Required]
        [Key]
        public int RequestId { get; set; }

        public string Location { get; set; }

        public string OptionalLocation { get; set; }

        public string Phone { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime RequesTime { get; set; }

        public DateTime FinishTime { get; set; }

        public string Status { get; set; }

        public string IsFinish { get; set; }

        public string IsFinishDoctor { get; set; }


        public RequestForService()
        {
            Name = "Not mentioned";
            Email = "Not mentioned";
            RequesTime = DateTime.Now;
            FinishTime = DateTime.Now;
            Status = "Pending";
            IsFinish = "false";
            IsFinishDoctor = "false";

        }


    }
}
