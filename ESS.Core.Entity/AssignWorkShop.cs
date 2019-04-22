using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESS.Core.Entity
{
  public  class AssignWorkShop
    {
        [Required]
        [Key]
        public int AssignWorkShopId { get; set; }

        public int WorkShopId { get; set; }

        public int UserId { get; set; }

        public String IsGoing { get; set; }

        public AssignWorkShop()
        {
            IsGoing = "false";
        }

    }
}
