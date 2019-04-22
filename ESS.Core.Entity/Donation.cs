using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESS.Core.Entity
{
    public class Donation
    {
        [Required]
        [Key]
        public int DonationId { get; set; }

        public double Amount { get; set; }

        public string CardNumber { get; set; }

        public DateTime DonationDate { get; set; }


        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public Donation()
        {
            Name = "Not mentioned";
            Email = "Not mentioned";
            Phone = "Not mentioned";
            CardNumber = "Not mentioned";
            DonationDate = DateTime.Now;

        }

    }
}
