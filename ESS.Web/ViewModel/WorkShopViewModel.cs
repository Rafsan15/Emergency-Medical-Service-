using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ESS.Core.Entity;

namespace ESS.Web.ViewModel
{
    public class WorkShopViewModel
    {
        public List<WorkShop> workShops = new List<WorkShop>();

        public List<string> IsGoing = new List<string>();

        public List<int> Id = new List<int>();

    }
}