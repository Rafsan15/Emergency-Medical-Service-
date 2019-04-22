using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESS.Core.Entity
{
   public class RequestCount
    {
        [Key]
        public int CountId { get; set; }

        public int PreviousCount { get; set; }

        public int NewCount { get; set; }

        
    }
}
