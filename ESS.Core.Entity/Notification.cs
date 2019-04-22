using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESS.Core.Entity
{
   public class Notification
    {
        [Required]
        [Key]
        public int NotificationId { get; set; }

        public int RequestId { get; set; }

        public int WorkShopId { get; set; }

        public int UserId { get; set; }

        public string IsDeliver { get; set; }

        public string IsWorkShop { get; set; }

        public Notification()
        {
            UserId = 0;
            WorkShopId = 0;
            RequestId = 0;
            IsDeliver = "false";
            IsWorkShop = "false";

        }


    }
}
