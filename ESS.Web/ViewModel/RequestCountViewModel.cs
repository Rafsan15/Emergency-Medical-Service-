using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ESS.Core.Entity;

namespace ESS.Web.ViewModel
{
    public class RequestCountViewModel
    {
        public List<RequestForService> RequestForServices =new List<RequestForService>();
        public RequestCount RequestCount { get; set; }
    }
}