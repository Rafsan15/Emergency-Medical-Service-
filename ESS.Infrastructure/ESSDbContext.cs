using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESS.Core.Entity;

namespace ESS.Infrastructure
{
    public class ESSDbContext : DbContext
    {
        public ESSDbContext():base("ESSDBConnection")
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<VolunteerDoctor> VolunteerDoctors { get; set; }
        public DbSet<RequestForService> Requests { get; set; }
        public DbSet<AssignedRequest> Assigned { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<WorkShop> WorkShops { get; set; }
        public DbSet<AssignWorkShop> AssignWorkShops { get; set; }
        public DbSet<RequestCount> RequestCounts { get; set; }


    }
}
