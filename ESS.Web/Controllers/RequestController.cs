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
    public class RequestController : Controller
    {
        private IUserService _userService;
        private IEmployeeService _employeeService;
        private IRequestService _requestService;
        private IAssignedRequestService _assignedRequest;
        private IVolunteerService _volunteerService;
        private IVolunteerDoctorService _volunteerDoctorService;
        private INotificationService _notificationService;
        private IRequestCount _requestCount;

        public RequestController(UserService userService, EmployeeService employeeService,
            RequestService requestService, AssignedRequestService assignedRequest,
            VolunteerService volunteerService,RequestCountService requestCount,NotificationService notificationService, VolunteerDoctorService volunteerDoctor)
        {
            _userService = userService;
            _employeeService = employeeService;
            _requestService = requestService;
            _assignedRequest = assignedRequest;
            _volunteerService = volunteerService;
            _volunteerDoctorService = volunteerDoctor;
            _notificationService = notificationService;
            _requestCount = requestCount;
        }

        // GET: Request
        public ActionResult AddRequest()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddRequest(RequestForService request)
        {
            try
            {
                var result = _requestService.Save(request);
                Notification notification=new Notification();
                notification.RequestId = request.RequestId;
                var result2 = _notificationService.Save(notification);
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
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
            return RedirectToAction("Index", "Home", new {count = 1 });
        }

        public ActionResult GetAllRequest()
        {
            try
            {
                RequestCountViewModel requestCountView=new RequestCountViewModel();
                var result = _requestService.GetAll();
                var requests = result.Data.Where(q => q.Status.Equals("Pending")).ToList();
                if (result.HasError)
                {
                    ViewBag.Message = result.Message;
                    return Content(result.Message);
                }

                var notification = _notificationService.GetAll();
                var list = notification.Data.Where(q => q.UserId == 0 && q.IsDeliver.Equals("false")).ToList();
                if (list.Count != 0)
                {
                    ViewBag.notification = true;
                    ViewBag.notificationCount = list.Count;
                }
                else
                {
                    ViewBag.notification = false;
                }
                ViewBag.Entry =HttpUtil.CurrentUser.UserType;
                RequestCount req= new RequestCount();
                req.NewCount = result.Data.Count;
                var request = _requestCount.GetById(1);
                request.Data.NewCount = result.Data.Count;
                request.Data.PreviousCount = request.Data.NewCount;
                var request2 = _requestCount.Save(request.Data);
                requestCountView.RequestForServices = requests;
                requestCountView.RequestCount = request.Data;

                return View(requestCountView);
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }

        }

        public ActionResult RequestDetails(int id , int count=0)
        {
            try
            {
                ListViewModel list = new ListViewModel();
                var result = _requestService.GetById(id);
                if (result.HasError)
                {
                    ViewBag.Message = result.Message;
                    return Content(result.Message);
                }
                list.Request = result.Data;
                result.Data.Status = "Working";
                _requestService.Save(result.Data);
                var result2 = _volunteerService.GetAll();
                list.Volunteers = result2.Data.Where(q => q.IsActive.Equals("true") && q.Area.Equals(result.Data.OptionalLocation)).ToList();
                foreach (var p in list.Volunteers)
                {
                    list.Users.Add(_userService.GetById(p.UserId).Data);
                }
                if (result2.HasError)
                {
                    ViewBag.Message = result2.Message;
                    return Content(result2.Message);
                }    
                list.Request = result.Data;
                ViewBag.count = count;

                var assigned = _assignedRequest.GetAll(id.ToString());
                if (assigned.Data.Count!=0)
                {
                    foreach (var p in assigned.Data)
                    {
                        if (p.VolunteerDoctorId != 0)
                        {
                            var doc = _volunteerDoctorService.GetById(p.VolunteerDoctorId).Data;
                            var user = _userService.GetById(doc.UserId).Data;
                            UserDoctorViewModel uv = new UserDoctorViewModel();
                            uv.UserId = user.UserId;
                            uv.Email = user.Email;
                            uv.Name = user.Name;
                            uv.Phone = user.Phone;
                            uv.SpecialDomain = doc.SpecialDomain;
                            uv.CurrentLocation = doc.CurrentLocation;
                            list.UserDoctorViewModel.Add(uv);
                        }
                       

                    }
                    foreach (var p in assigned.Data)
                    {
                        if (p.VolunteerId != 0)
                        {
                            var vol = _volunteerService.GetById(p.VolunteerId).Data;
                            var user = _userService.GetById(vol.UserId).Data;
                            UserVolunteerViewModel uv = new UserVolunteerViewModel();
                            uv.UserId = user.UserId;
                            uv.Email = user.Email;
                            uv.Name = user.Name;
                            uv.Phone = user.Phone;
                            uv.JobDetails = vol.JobDetails;
                            uv.CurrentLocation = vol.CurrentLocation;
                            list.UserVolunteerViewModels.Add(uv);
                        }
                       

                    }
                    ViewBag.hasvolunteer = 1;

                }
                ViewBag.Entry = HttpUtil.CurrentUser.UserType;
                var notification = _notificationService.GetById(id);
                if(notification.Data.UserId==0)
                    notification.Data.IsDeliver = "true";
                var result3 = _notificationService.Save(notification.Data);
                if (result3.HasError)
                {
                    ViewBag.Message = result3.Message;
                    return Content(result3.Message);
                }
                return View(list);
            }
            catch (Exception e)
            {
                return Content(e.Message);

            }

        }

        [HttpPost]
        public ActionResult UpdateRequest(RequestForService request)
        {
            try
            {
                request.Status = "handling";
                var result = _requestService.Save(request);
                if (result.HasError)
                {
                    ViewBag.Message = result.Message;
                    return Content(result.Message);
                }

                return Content("Hello");
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        public ActionResult DiscardRequest(int id)
        {
            try
            {
                var result = _requestService.Delete(id);
                var result2 = _assignedRequest.GetAll(id.ToString());
                foreach (var p in result2.Data)
                {
                    _assignedRequest.Delete(p.AssignedId);
                  /*  if (p.VolunteerId!=0)
                    {
                        var volunteerobj = _volunteerService.GetById(p.VolunteerId);
                        volunteerobj.Data.IsActive = "true";
                        volunteerobj.Data.IsVolunteerDone = "Done";
                        var result3 = _volunteerService.Save(volunteerobj.Data);
                    }
                    if (p.VolunteerDoctorId != 0)
                    {
                        var volunteerobj = _volunteerDoctorService.GetById(p.VolunteerDoctorId);
                        volunteerobj.Data.IsActive = "true";
                        volunteerobj.Data. IsDoctorDone = "Done";
                        var result3 = _volunteerDoctorService.Save(volunteerobj.Data);
                    }*/

                }
                return RedirectToAction("GetAllRequest", "Request");

            }
            catch (Exception e)
            {
                return Content(e.Message);

            }

        }

        public ActionResult ApproveRequest(int id)
        {
            try
            {
                var result = _requestService.GetById(id);
                result.Data.Status = "approved";
                result.Data.FinishTime = DateTime.Now;
                var result2 = _requestService.Save(result.Data);
                if (result.HasError)
                {
                    ViewBag.Message = result.Message;
                    return Content(result.Message);
                }
                return RedirectToAction("GetAllRequest", "Request");

            }
            catch (Exception e)
            {
                return Content(e.Message);

            }

        }

      /*  public ActionResult AssignedRequest()
        {
            try
            {
                var result = _volunteerService.GetAll();
                var result2 = _volunteerDoctorService.GetAll();
                var requestModel = new RequestViewModel();
                requestModel.Volunteers = result.Data.Where(q => q.IsActive.Equals("true")).ToList();
                requestModel.VolunteerDoctors = result2.Data.Where(q => q.IsActive.Equals("true")).ToList();
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

            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
            return Content("Hello");
        }
        */

        public ActionResult AssignedRequests(int requestId , int volunteerId , int empId)
        {
            try
            {
                AssignedRequest assign= new AssignedRequest();
                assign.RequestId = requestId;
                assign.VolunteerId = volunteerId;
                assign.EmployeeId = empId;
                var result2 = _volunteerService.GetById(volunteerId);
                result2.Data.IsActive = "false";
                result2.Data.IsVolunteerDone = "Not Done";
                _volunteerService.Save(result2.Data);
                var result = _assignedRequest.Save(assign);
                ViewBag.count = 1;
                if (result.HasError)
                {
                    return Content(result.Message);
                }
                Notification notification = new Notification();
                notification.RequestId = requestId;
                notification.UserId = volunteerId;
                var result3 = _notificationService.Save(notification);
                if (result3.HasError)
                {
                    ViewBag.Message = result3.Message;
                    return Content(result3.Message);
                }

            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
            return RedirectToAction("RequestDetails","Request", new { id = requestId , count=1 });
        }

        public ActionResult RequestDetailsDoctor(int id, int count=0)
        {
            try
            {
                var result = _requestService.GetById(id);
                if (result.HasError)
                {
                    ViewBag.Message = result.Message;
                    return Content(result.Message);
                }

                var result3 = _volunteerDoctorService.GetAll();
                ListViewModel list = new ListViewModel();
                list.VolunteerDoctors = result3.Data.Where(q => q.IsActive.Equals("true")).ToList();
                foreach (var p in list.VolunteerDoctors)
                {
                    var user = _userService.GetById(p.UserId).Data;
                    UserDoctorViewModel uv = new UserDoctorViewModel();
                    uv.UserId = p.UserId;
                    uv.Email = user.Email;
                    uv.Name = user.Name;
                    uv.Phone = user.Phone;
                    uv.SpecialDomain = p.SpecialDomain;
                    list.UserDoctorViewModel.Add(uv);

                }
                if (result3.HasError)
                {
                    ViewBag.Message = result3.Message;
                    return Content(result3.Message);
                }
               
                list.Request = result.Data;
                ViewBag.count = count;
                ViewBag.Entry = HttpUtil.CurrentUser.UserType;

                return View(list);
            }
            catch (Exception e)
            {
                return Content(e.Message);

            }

        }

        public ActionResult AssignedRequestsDoctor(int requestId, int doctorId,int empId)
        {
            try
            {
                AssignedRequest assign = new AssignedRequest();
                assign.RequestId = requestId;
                assign.VolunteerDoctorId = doctorId;
                assign.EmployeeId = empId;
                var result2 = _volunteerDoctorService.GetById(doctorId);
                if (result2.HasError)
                {
                    ViewBag.Message = result2.Message;
                    return Content(result2.Message);
                }
                result2.Data.IsActive = "false";
                result2.Data.IsDoctorDone = "Not Done";
                _volunteerDoctorService.Save(result2.Data);
                var result = _assignedRequest.Save(assign);
                if (result.HasError)
                {
                    ViewBag.Message = result.Message;
                    return Content(result.Message);
                }
                ViewBag.count = 1;
                Notification notification = new Notification();
                notification.RequestId = requestId;
                notification.UserId = doctorId;
                var result3 = _notificationService.Save(notification);
                if (result3.HasError)
                {
                    ViewBag.Message = result3.Message;
                    return Content(result3.Message);
                }
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
            return RedirectToAction("RequestDetails", "Request", new { id = requestId, count = 1 });
        }

        public ActionResult GetAllWorkingRequest()
        {
            try
            {
                var result = _requestService.GetAll();
                var requests = result.Data.Where(q => q.Status.Equals("Working")).ToList();
                if (result.HasError)
                {
                    ViewBag.Message = result.Message;
                    return Content(result.Message);
                }
                ViewBag.Entry = HttpUtil.CurrentUser.UserType;

                return View(requests);
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }

        }

        public ActionResult RequestDetailsWorking(int id, int count = 0)
        {
            try
            {
                ListViewModel list = new ListViewModel();
                var result = _requestService.GetById(id);
                if (result.HasError)
                {
                    ViewBag.Message = result.Message;
                    return Content(result.Message);
                }
                
                list.Request = result.Data;
                ViewBag.count = count;

                var assigned = _assignedRequest.GetAll(id.ToString());
               
                    foreach (var p in assigned.Data)
                    {
                        var doc = _volunteerDoctorService.GetVolunteerDoctorById(p.VolunteerDoctorId).Data;
                        var user = _userService.GetById(doc.UserId).Data;
                        UserDoctorViewModel uv = new UserDoctorViewModel();
                        uv.UserId = user.UserId;
                        uv.Email = user.Email;
                        uv.Name = user.Name;
                        uv.Phone = user.Phone;
                        uv.SpecialDomain = doc.SpecialDomain;
                        list.UserDoctorViewModel.Add(uv);

                    }
                    foreach (var p in assigned.Data)
                    {
                        var vol = _volunteerService.GetVolunteerById(p.VolunteerDoctorId).Data;
                        var user = _userService.GetById(vol.UserId).Data;
                        UserVolunteerViewModel uv = new UserVolunteerViewModel();
                        uv.UserId = user.UserId;
                        uv.Email = user.Email;
                        uv.Name = user.Name;
                        uv.Phone = user.Phone;
                        uv.JobDetails = vol.JobDetails;
                        list.UserVolunteerViewModels.Add(uv);

                    }
                

                return Content("dd");
            }
            catch (Exception e)
            {
                return Content(e.Message);

            }

        }

        public ActionResult RemoveAssignVolunteer(int requestId , int userId)
        {
            try
            {
                var assign = _assignedRequest.GetAll(requestId.ToString());
                foreach (var p in assign.Data)
                {
                    if (p.VolunteerId == userId)
                    {
                        p.VolunteerId = 0;
                        var result = _assignedRequest.Save(p);
                        if (result.HasError)
                        {
                            ViewBag.Message = result.Message;
                            return Content(result.Message);
                        }
                    }
                   
                }
                
                var volunteer = _volunteerService.GetById(userId);
                volunteer.Data.IsActive = "true";
                volunteer.Data.IsVolunteerDone = "Done";
                var result2 = _volunteerService.Save(volunteer.Data);
                if (result2.HasError)
                {
                    ViewBag.Message = result2.Message;
                    return Content(result2.Message);
                }

                var notification = _notificationService.GetAllByRequest(requestId);
                foreach (var p in notification.Data)
                {
                    if (p.UserId == userId)
                    {
                        _notificationService.Delete(p.NotificationId);
                    }
                }

            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
            return RedirectToAction("RequestDetails" , new { id = requestId });
        }

        public ActionResult RemoveAssignVolunteerDoctor(int requestId, int userId)
        {
            try
            {
                var assign = _assignedRequest.GetAll(requestId.ToString());
                foreach (var p in assign.Data)
                {
                    if (p.VolunteerDoctorId == userId)
                    {
                        p.VolunteerDoctorId = 0;
                        var result = _assignedRequest.Save(p);
                        if (result.HasError)
                        {
                            ViewBag.Message = result.Message;
                            return Content(result.Message);
                        }
                    }
                }
                var VolunteerDoctor = _volunteerDoctorService.GetById(userId);
                VolunteerDoctor.Data.IsActive = "true";
                VolunteerDoctor.Data.IsDoctorDone = "Done";
                var result2 = _volunteerDoctorService.Save(VolunteerDoctor.Data);
                if (result2.HasError)
                {
                    ViewBag.Message = result2.Message;
                    return Content(result2.Message);
                }
                var notification = _notificationService.GetAllByRequest(requestId);
                foreach (var p in notification.Data)
                {
                    if (p.UserId == userId)
                    {
                        _notificationService.Delete(p.NotificationId);
                    }
                }

            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
            return RedirectToAction("RequestDetails", new { id = requestId });
        }






    }
}