using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Framework;
using ESS.Core.Entity;
using ESS.Core.Service;
using ESS.Service.Interface;
using ESS.Web.DatabaseHelper;
using ESS.Web.ViewModel;

namespace ESS.Web.Controllers
{
    public class VolunteerDoctorController : Controller
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



        public VolunteerDoctorController(UserService userService, EmployeeService employeeService,
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

        // GET: Volunteer
        public ActionResult AddVolunteerDoctor()
        {
            ViewBag.Entry = HttpUtil.CurrentUser.UserType;
            return View();
        }

        [HttpPost]
        public ActionResult AddVolunteerDoctor(SignUpViewModel signUpViewModel)
        {
            try
            {
                var user = new User();
                var volunteerDoctor = new VolunteerDoctor();
                var signuphelper = new SignUpHelper();
                user = signuphelper.GetUserobj(signUpViewModel);
                var result = _userService.Save(user);
                var userid = _userService.GetLastId(signUpViewModel.Email);
                signUpViewModel.UserId = userid.Data;
                volunteerDoctor = signuphelper.GetVolunteerDoctorobj(signUpViewModel);
                var result2 = _volunteerDoctorService.Save(volunteerDoctor);
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
                return RedirectToAction("GetAllVolunteerDoctor");


            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        // GET
        public ActionResult GetAllVolunteerDoctor()
        {
            try
            {
                var result = _volunteerDoctorService.GetAll();
                ListViewModel list = new ListViewModel();
                list.VolunteerDoctors = result.Data;
                foreach (var p in list.VolunteerDoctors)
                {
                    var user=_userService.GetById(p.UserId).Data;
                    UserDoctorViewModel uv=new UserDoctorViewModel();
                    uv.UserId = p.UserId;
                    uv.Email = user.Email;
                    uv.Name = user.Name;
                    uv.Phone = user.Phone;
                    uv.SpecialDomain = p.SpecialDomain;
                    uv.IsActive = p.IsActive;
                    uv.Area = p.Area;
                    uv.CurrentLocation = p.CurrentLocation;

                    list.UserDoctorViewModel.Add(uv);

                }
                if (result.HasError)
                {
                    ViewBag.Message = result.Message;
                    return Content(result.Message);
                }
                ViewBag.Entry = HttpUtil.CurrentUser.UserType;

                return View(list);
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }

        }

        public ActionResult VolunteerDoctorDetails(int id)
        {
            try
            {
                var result = _volunteerDoctorService.GetById(id);
                var result2 = _userService.GetById(id);
                DetailsViewModel details = new DetailsViewModel();
                SignUpViewModel sg = new SignUpViewModel();
                sg = details.SignUpViewModel(result.Data, result2.Data);
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
                if (result.HasError)
                {
                    ViewBag.Message = result.Message;
                    return Content(result.Message);
                }
                ViewBag.Entry = HttpUtil.CurrentUser.UserType;

                return View(sg);
            }
            catch (Exception e)
            {
                return Content(e.Message);

            }

        }

        [HttpPost]
        public ActionResult UpdateVolunteerDoctor(SignUpViewModel signUp)
        {
            try
            {
                var volunteerdoctorobj = _volunteerDoctorService.GetById(signUp.UserId);
                var userobj = _userService.GetById(signUp.UserId);
                var update = new UpdateInstance();
                var updateUser = update.GetUpdatedUserObj(signUp, userobj.Data);
                var result = _userService.Save(updateUser);

                var updatevolunteerdoctor = update.GetUpdatedVolunteerDoctorObj(signUp, volunteerdoctorobj.Data);
                var result2 = _volunteerDoctorService.Save(updatevolunteerdoctor);
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
                return RedirectToAction("GetAllVolunteerDoctor");
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        public ActionResult DeleteVolunteerDoctor(int id)
        {
            try
            {
                var result = _volunteerDoctorService.Delete(id);
                var result2 = _userService.Delete(id);
                var assign = _assignedRequest.GetDoctorAll(id);
                foreach (var p in assign.Data)
                {
                    _assignedRequest.Delete(p.AssignedId);
                }
                var workShop = _assignworkShopService.GetAllByUserId(id);
                foreach (var p in workShop.Data)
                {
                    _assignworkShopService.Delete(p.AssignWorkShopId);
                }
                var notification = _notificationService.GetAllByUserId(id);
                foreach (var p in notification.Data)
                {
                    _notificationService.Delete(p.NotificationId);
                }
                return RedirectToAction("GetAllVolunteerDoctor");

            }
            catch (Exception e)
            {
                return Content(e.Message);
            }

        }


        public ActionResult VolunteerDoctorActivities()
        {
            try
            {
                var result = _assignedRequest.GetDoctorAll(HttpUtil.CurrentUser.UserId);
                List<RequestForService> request = new List<RequestForService>();
                foreach (var p in result.Data)
                {
                    request.Add(_requestService.GetById(p.RequestId).Data);
                }
                request = request.Where(q => q.IsFinishDoctor.Equals("false")).ToList();
                ViewBag.Entry = HttpUtil.CurrentUser.UserType;
                var notifi = _notificationService.GetAll();
                var notification = notifi.Data.Where(q => q.UserId == HttpUtil.CurrentUser.UserId && q.IsDeliver.Equals("false")).ToList();
                if (notification.Count != 0)
                {
                    ViewBag.notification = true;
                    ViewBag.notificationCount = notification.Count;
                }
                else
                {
                    ViewBag.notification = false;
                }
                return View(request);

            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        public ActionResult FinishRequestDoctor(int id)
        {
            try
            {

                var request = _requestService.GetById(id);
                request.Data.IsFinishDoctor = "true";
                request.Data.FinishTime = DateTime.Now;
                var result = _requestService.Save(request.Data);
                if (result.HasError)
                {
                    ViewBag.Message = result.Message;
                    return Content(result.Message);
                }

                var volunteerdocobj = _volunteerDoctorService.GetById(HttpUtil.CurrentUser.UserId);
                volunteerdocobj.Data.IsActive = "true";
                volunteerdocobj.Data.IsDoctorDone = "Done";

                var result2 = _volunteerDoctorService.Save(volunteerdocobj.Data);
                if (result2.HasError)
                {
                    ViewBag.Message = result2.Message;
                    return Content(result2.Message);
                }
                var notification = _notificationService.GetAllByRequest(id);
                foreach (var p in notification.Data)
                {
                    if (p.UserId == HttpUtil.CurrentUser.UserId)
                    {
                        p.IsDeliver = "true";
                        var result3 = _notificationService.Save(p);
                        if (result3.HasError)
                        {
                            ViewBag.Message = result3.Message;
                            return Content(result3.Message);
                        }
                    }

                }
                return RedirectToAction("VolunteerDoctorActivities");
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        public ActionResult PreviousActivitiesDoctor()
        {
            try
            {
                var result = _assignedRequest.GetDoctorAll(HttpUtil.CurrentUser.UserId); 
                //var result = _assignedRequest.GetAll(19);
                List<RequestForService> request = new List<RequestForService>();
                foreach (var p in result.Data)
                {
                    request.Add(_requestService.GetById(p.RequestId).Data);
                }

                request = request.Where(q => q.IsFinishDoctor.Equals("true")).ToList();
                return View(request);

            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        public ActionResult UpdateVolunteerLocation()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UpdateVolunteerLocation(UpdateVolunteerLocation loc)
        {
            try
            {
                var result = _volunteerDoctorService.GetById(HttpUtil.CurrentUser.UserId);
                result.Data.CurrentLocation = loc.Location;
                result.Data.Area = loc.OptionalLocation;
                _volunteerDoctorService.Save(result.Data);
                if (result.HasError)
                {
                    ViewBag.Message = result.Message;
                    return Content(result.Message);

                }
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
            return RedirectToAction("VolunteerDoctorActivities", "VolunteerDoctor");
        }

    }
}