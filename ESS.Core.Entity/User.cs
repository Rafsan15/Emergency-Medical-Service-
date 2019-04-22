using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESS.Core.Entity
{
    public class User
    {

        [Required]
        [Key]
        public int UserId { get; set; }

        public string Name { get; set; }

        [Index(IsUnique = true)]
        [StringLength(255)]
        public string Email { get; set; }

        public string Password { get; set; }

        public string Address { get; set; }

        [Index(IsUnique = true)]
        [StringLength(25)]
        public string Phone { get; set; }


        public string Photo { get; set; }

        public string UserType { get; set; }

        [Required]
        public DateTime DOB { get; set; }

        [Required]
        public DateTime JoinDate { get; set; }

        public User()
        {
            
            JoinDate = DateTime.Now;
            DOB= DateTime.Now;
            
        }


    }
}
