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
using Framework;

namespace ESS.Web.Controllers
{
    public class VolunteerController : Controller
    {
        private IUserService _userService;
        private IEmployeeService _employeeService;
        private IVolunteerService _volunteerService;
        private IRequestService _requestService;
        private IAssignedRequestService _assignedRequest;
        private INotificationService _notificationService;
        private IWorkShopServiceInterface _workshopService;
        private IAssignworkShopServiceinterface _assignworkShopService;

        public VolunteerController(UserService userService, EmployeeService employeeService, 
            VolunteerService volunteerService, WorkShopService shopService, AssignWorkShopService assignWorkShopService, NotificationService notificationService, RequestService requestService, AssignedRequestService assignedRequest)
        {
            _userService = userService;
            _employeeService = employeeService;
            _volunteerService = volunteerService;
            _requestService = requestService;
            _assignedRequest = assignedRequest;
            _notificationService = notificationService;
            _workshopService = shopService;
            _assignworkShopService = assignWorkShopService;
        }

        // GET: Volunteer
        public ActionResult AddVolunteer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddVolunteer(SignUpViewModel signUpViewModel)
        {
            try
            {
                var user = new User();
                var volunteer = new Volunteer();
                var signuphelper = new SignUpHelper();
                user = signuphelper.GetUserobj(signUpViewModel);
                var result = _userService.Save(user);
                var userid = _userService.GetLastId(signUpViewModel.Email);
                signUpViewModel.UserId = userid.Data;
                volunteer = signuphelper.GetVolunteerobj(signUpViewModel);
                var result2 = _volunteerService.Save(volunteer);
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
                return RedirectToAction("Index","Home");

            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        public ActionResult GetAllVolunteer()
        {
            try
            {
                var result = _volunteerService.GetAll();
                ListViewModel list=new ListViewModel();
                list.Volunteers = result.Data;
                foreach (var p in list.Volunteers)
                {
                    var user = _userService.GetById(p.UserId).Data;
                    UserVolunteerViewModel uv = new UserVolunteerViewModel();
                    uv.UserId = user.UserId;
                    uv.Email = user.Email;
                    uv.Name = user.Name;
                    uv.Phone = user.Phone;
                    uv.JobDetails = p.JobDetails;
                    uv.IsActive = p.IsActive;
                    uv.IsActive = p.IsActive;
                    uv.CurrentLocation = p.CurrentLocation;
                    uv.Area = p.Area;
                    list.UserVolunteerViewModels.Add(uv);

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

        public ActionResult VolunteerDetails(int id)
        {
            try
            {
                var result = _volunteerService.GetById(id);
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
        public ActionResult UpdateVolunteer(SignUpViewModel signUp)
        {
            try
            {
                var volunteerobj = _volunteerService.GetById(signUp.UserId);
                var userobj = _userService.GetById(signUp.UserId);
                var update = new UpdateInstance();
                var updateUser = update.GetUpdatedUserObj(signUp, userobj.Data);
                var result = _userService.Save(updateUser);

                var updateVolunteer = update.GetUpdatedVolunteerObj(signUp, volunteerobj.Data);
                var result2 = _volunteerService.Save(updateVolunteer);
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
                return RedirectToAction("GetAllVolunteer");
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        public ActionResult DeleteVolunteer(int id)
        {
            try
            {
                var result = _volunteerService.Delete(id);
                var result2 = _userService.Delete(id);
                var assign = _assignedRequest.GetVoluenteerAll(id);
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

                return RedirectToAction("GetAllVolunteer");


            }
            catch (Exception e)
            {
                return Content(e.Message);

            }

        }


        public ActionResult GetAllVolunteerRequest()
        {
            try
            {
                var result = _volunteerService.GetAll();
                ListViewModel list = new ListViewModel();
                list.Volunteers = result.Data.Where(q => q.IsApprove.Equals("false")).ToList();
                foreach (var p in list.Volunteers)
                {
                    var user = _userService.GetById(p.UserId).Data;
                    UserVolunteerViewModel uv = new UserVolunteerViewModel();
                    uv.UserId = p.UserId;
                    uv.Email = user.Email;
                    uv.Name = user.Name;
                    uv.Phone = user.Phone;
                    uv.Address = user.Address;
                    uv.Gender = user.Address;
                    uv.JobDetails = p.JobDetails;
                    list.UserVolunteerViewModels.Add(uv);

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

        public ActionResult ApproveVolunteer(int id)
        {
            try
            {
                var volunteerobj = _volunteerService.GetById(id);
                volunteerobj.Data.IsApprove = "true";

                  var result2 = _volunteerService.Save(volunteerobj.Data);
              
                if (result2.HasError)
                {
                    ViewBag.Message = result2.Message;
                    return Content(result2.Message);
                }
                return RedirectToAction("GetAllVolunteerRequest");
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        public ActionResult VolunteerActivities()
        {
            try
            {
                var result = _assignedRequest.GetVoluenteerAll(HttpUtil.CurrentUser.UserId);
               // var result = _assignedRequest.GetAll(19);
                List<RequestForService>request=new List<RequestForService>();
                foreach (var p in result.Data)
                {
                    request.Add(_requestService.GetById(p.RequestId).Data);
                }
                request = request.Where(q => q.IsFinish.Equals("false")).ToList();
                ViewBag.Entry = HttpUtil.CurrentUser.UserType;
                var notifi = _notificationService.GetAll();
                var  notification = notifi.Data.Where(q => q.UserId == HttpUtil.CurrentUser.UserId &&q.IsDeliver.Equals("false")).ToList();
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

        public ActionResult FinishRequest(int id)
        {
            try
            {
               
                var request = _requestService.GetById(id);
                request.Data.IsFinish = "true";
                request.Data.FinishTime=DateTime.Now;
                var result = _requestService.Save(request.Data);
                if (result.HasError)
                {
                    ViewBag.Message = result.Message;
                    return Content(result.Message);
                }

                var volunteerobj = _volunteerService.GetById(HttpUtil.CurrentUser.UserId);
                volunteerobj.Data.IsActive = "true";
                volunteerobj.Data.IsVolunteerDone = "Done";
                var result2 = _volunteerService.Save(volunteerobj.Data);
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
                
                return RedirectToAction("VolunteerActivities");
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        public ActionResult PreviousActivities()
        {
            try
            {
                var result = _assignedRequest.GetVoluenteerAll(HttpUtil.CurrentUser.UserId);
                //var result = _assignedRequest.GetAll(19);
                List<RequestForService> request = new List<RequestForService>();
                foreach (var p in result.Data)
                {
                    request.Add(_requestService.GetById(p.RequestId).Data);
                }

                request = request.Where(q => q.IsFinish.Equals("true")).ToList();
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
                var result = _volunteerService.GetById(HttpUtil.CurrentUser.UserId);
                result.Data.CurrentLocation = loc.Location;
                result.Data.Area=loc.OptionalLocation;
                _volunteerService.Save(result.Data);
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
            return RedirectToAction("VolunteerActivities","Volunteer");
        }

        public ActionResult CloseNotification()
        {
            try
            {
                var notification = _notificationService.GetAll();
                foreach (var p in notification.Data)
                {
                    if (p.UserId == HttpUtil.CurrentUser.UserId && p.IsDeliver.Equals("false"))
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
                
                
               
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
            return RedirectToAction("VolunteerActivities");
        }
    }
}