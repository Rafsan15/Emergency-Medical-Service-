﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ESS.Core.Entity;

namespace ESS.Web.ViewModel
{
    public class RequestViewModel
    {
        public List<Volunteer> Volunteers { get; set; }

        public List<VolunteerDoctor> VolunteerDoctors { get; set; }
    }
}