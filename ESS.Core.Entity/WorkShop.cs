using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESS.Core.Entity
{
  public  class WorkShop
    {
        [Required]
        [Key]
        public int WorkShopId { get; set; }

        public string Name { get; set; }

        public string Venue { get; set; }

        public string Details { get; set; }

        public int Phone { get; set; }

        public string WorkShopTime { get; set; }

        public DateTime WorkShopDate { get; set; }

        public string IsFinish { get; set; }

        public WorkShop()
        {
            IsFinish = "false";
        }


    }
}
