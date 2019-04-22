using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ESS.Core.Service;
using ESS.Service.Interface;
using Unity;

namespace ESS.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            IUnityContainer container = new UnityContainer();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IEmployeeService, EmployeeService>();
            container.RegisterType<IVolunteerDoctorService, VolunteerDoctorService>();
            container.RegisterType<IVolunteerService, VolunteerService>();
            container.RegisterType<IDonationService, DonationService>();
            container.RegisterType<IRequestService, RequestService>();
            container.RegisterType<IAssignedRequestService, AssignedRequestService>();
            container.RegisterType<IAuthenticationService, AuthenticationService>();
            container.RegisterType<INotificationService, NotificationService>();
            container.RegisterType<IWorkShopServiceInterface, WorkShopService>();
            container.RegisterType<IAssignworkShopServiceinterface, AssignWorkShopService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
