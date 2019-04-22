using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ESS.Core.Entity;
using ESS.Core.Service;
using ESS.Service.Interface;
using ESS.Web.DatabaseHelper;
using ESS.Web.ViewModel;

namespace ESS.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private IUserService _userService;
        private IEmployeeService _employeeService;
        private IVolunteerService _volunteerService;
        private IVolunteerDoctorService _volunteerDoctorService;
        private IRequestService _requestService;
        private IAssignedRequestService _assignedRequest;
        private INotificationService _notificationService;
        private IWorkShopServiceInterface _workshopService;
        private IAssignworkShopServiceinterface _assignworkShopService;



        public EmployeeController(UserService userService, EmployeeService employeeService,
            VolunteerService volunteerService, WorkShopService shopService, AssignWorkShopService assignWorkShopService, NotificationService notificationService, VolunteerDoctorService volunteerDoctor,
            RequestService requestService, AssignedRequestService assignedRequest)
        {
            _userService = userService;
            _employeeService = employeeService;
            _volunteerService = volunteerService;
            _volunteerDoctorService = volunteerDoctor;
            _requestService = requestService;
            _assignedRequest = assignedRequest;
            _notificationService = notificationService;
            _workshopService = shopService;
            _assignworkShopService = assignWorkShopService;

        }

        // GET: Employee
        public ActionResult AddEmployee()
        {

            return View();
        }

        [HttpPost]
        public ActionResult AddEmployee(SignUpViewModel signUpViewModel)
        {
            try
            {
                var user = new User();
                var employee = new Employee();
                var signuphelper = new SignUpHelper();
                user = signuphelper.GetUserobj(signUpViewModel);
                var result = _userService.Save(user);
                var userid = _userService.GetLastId(signUpViewModel.Email);
                signUpViewModel.UserId = userid.Data;
                employee = signuphelper.GetEmployeeobj(signUpViewModel);
                var result2 = _employeeService.Save(employee);
                if (result.HasError)
                {
                    ViewBag.Message = result.Message;
                    return Content(result.Message);
                }
                if (result2.HasError)
                {
                    ViewBag.Message = result2.Message;
                    return Content(result2.Message);
                }
                return RedirectToAction("GetAllEmployee");

            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        public ActionResult GetAllEmployee()
        {
            try
            {
                var result = _employeeService.GetAll();
                ListViewModel list = new ListViewModel();
                list.Employees = result.Data;
                var result2 = _userService.GetAll();
                list.Users = result2.Data.Where(q => q.UserType.Equals("Employee")).ToList();
                if (result.HasError)
                {
                    ViewBag.Message = result.Message;
                    return Content(result.Message);
                }
                return View(list);
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }

        }

        public ActionResult EmployeeDetails(int id)
        {
            try
            {
                var result = _employeeService.GetById(id);
                var result2 = _userService.GetById(id);
                DetailsViewModel details=new DetailsViewModel();
                SignUpViewModel sg=new SignUpViewModel();
                sg=details.SignUpViewModel(result.Data, result2.Data);
                if (result.HasError)
                {
                    ViewBag.Message = result.Message;
                    return Content(result.Message);
                }
                if (result2.HasError)
                {
                    ViewBag.Message = result2.Message;
                    return Content(result2.Message);
                }
                return View(sg);
            }
            catch (Exception e)
            {
                return Content(e.Message);

            }

        }

        [HttpPost]
        public ActionResult UpdateEmployee(SignUpViewModel signUp)
        {
            try
            {
                var employeeobj = _employeeService.GetById(signUp.UserId);
                var userobj = _userService.GetById(signUp.UserId);
                var update = new UpdateInstance();
                var updateUser = update.GetUpdatedUserObj(signUp, userobj.Data);
                var result = _userService.Save(updateUser);

                var updateEmployee = update.GetUpdatedEmployeeObj(signUp, employeeobj.Data);
                var result2 = _employeeService.Save(updateEmployee);
                if (result.HasError)
                {
                    ViewBag.Message = result.Message;
                    return Content(result.Message);
                }
                if (result2.HasError)
                {
                    ViewBag.Message = result.Message;
                    return Content(result.Message);
                }
                return RedirectToAction("GetAllEmployee");
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        public ActionResult DeleteEmployee(int id)
        {
            try
            {
                var result = _employeeService.Delete(id);
                var result2 = _userService.Delete(id);
                var assign = _assignedRequest.GetEmployeeAll(id);
                foreach (var p in assign.Data)
                {
                    var x = _assignedRequest.GetById(p.RequestId);
                    x.Data.EmployeeId = 0;
                    _assignedRequest.Save(x.Data);
                }
                return RedirectToAction("GetAllEmployee");

            }
            catch (Exception e)
            {
                return Content(e.Message);

            }

        }
    }
}