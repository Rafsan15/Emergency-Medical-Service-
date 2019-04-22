using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Framework;
using ESS.Core.Entity;
using ESS.Core.Service;
using ESS.Service.Interface;
using ESS.Web.ViewModel;

namespace ESS.Web.Controllers
{
    public class WorkShopController : Controller
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



        public WorkShopController(UserService userService, EmployeeService employeeService,
            VolunteerService volunteerService, NotificationService notificationService, VolunteerDoctorService volunteerDoctor,
            RequestService requestService, AssignedRequestService assignedRequest, WorkShopService shopService, AssignWorkShopService assignWorkShopService)
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
    
        // GET: WorkShop
        public ActionResult AddWorkShop()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddWorkShop(WorkShop workShop)
        {
            try
            {
                var result = _workshopService.Save(workShop);
                if (result.HasError)
                {
                    return Content(result.Message);
                }
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
            return RedirectToAction("GetAllWorkShop");

        }

        public ActionResult GetAllWorkShop()
        {
            try
            {
                var result = _workshopService.GetAll();
                if (result.HasError)
                {
                    return Content(result.Message);

                }
                return View(result);

            }
            catch (Exception e)
            {
                return Content(e.Message);
            }

        }

        public ActionResult WorkShopDetails(int id)
        {
            try
            {
                var result = _workshopService.GetById(id);
                ListViewModel list=new ListViewModel();
                list.WorkShop = result.Data;
                var result2 = _volunteerService.GetAll();
                var volunteer = result2.Data.ToList();
                var result3 = _volunteerDoctorService.GetAll();
                var doctor = result3.Data.ToList();
                var assign = _assignworkShopService.GetAllByWorkShopId(id);
                foreach (var p in assign.Data)
                {
                    var obj = _userService.GetById(p.UserId);
                    if (obj.Data.UserType.Equals("Volunteer"))
                    {
                        Volunteer vol = _volunteerService.GetById(obj.Data.UserId).Data;
                        volunteer.Remove(vol);
                    }

                    else if (obj.Data.UserType.Equals("Doctor"))
                    {
                        VolunteerDoctor vol = _volunteerDoctorService.GetById(obj.Data.UserId).Data;
                        doctor.Remove(vol);
                    }
                }
                

                foreach (var p in doctor)
                {

                    var doc = _volunteerDoctorService.GetVolunteerDoctorById(p.DoctorId).Data;
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
                foreach (var p in volunteer)
                {

                    var vol = _volunteerService.GetVolunteerById(p.VolunteerId).Data;
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

        public ActionResult WorkShopAssign(int workShopId, int userId , int track=0)
        {
            try
            {
                AssignWorkShop assign=new AssignWorkShop();
                assign.WorkShopId =workShopId;
                assign.UserId =userId;
                var result = _assignworkShopService.Save(assign);
                if (track == 1)
                {
                    var result2 = _volunteerService.GetById(userId);
                    //result2.Data.WorkShopStatus = "true";
                    var result3 = _volunteerService.Save(result2.Data);
                    if (result2.HasError)
                    {
                        ViewBag.Message = result2.Message;
                        return Content(result2.Message);
                    }
                    if (result3.HasError)
                    {
                        ViewBag.Message = result3.Message;
                        return Content(result3.Message);
                    }
                }

                else if (track == 2)
                {
                    var result2 = _volunteerDoctorService.GetById(userId);
                    //result2.Data.WorkShopStatus = "true";
                    var result3 = _volunteerDoctorService.Save(result2.Data);
                    if (result2.HasError)
                    {
                        ViewBag.Message = result2.Message;
                        return Content(result2.Message);
                    }
                    if (result3.HasError)
                    {
                        ViewBag.Message = result3.Message;
                        return Content(result3.Message);
                    }
                }
                
                Notification notification=new Notification();
                notification.UserId = userId;
                notification.WorkShopId = workShopId;
                notification.IsWorkShop = "true";
                var result4 = _notificationService.Save(notification);
                if (result.HasError)
                {
                    ViewBag.Message = result.Message;
                    return Content(result.Message);
                }
               
                if (result4.HasError)
                {
                    ViewBag.Message = result4.Message;
                    return Content(result4.Message);
                }

            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
            return RedirectToAction("WorkShopDetails",new{id= workShopId });
        }

        public ActionResult WorkShopInformation()
        {
            try
            {
                var result = _assignworkShopService.GetAll();
                var info = result.Data.Where(q => q.UserId == HttpUtil.CurrentUser.UserId).ToList();
                WorkShopViewModel workShopsModel = new WorkShopViewModel();
                foreach (var p in info)
                {
                    var x = _workshopService.GetById(p.WorkShopId).Data;
                    if (x.IsFinish.Equals("false"))
                    {
                        workShopsModel.workShops.Add(x);
                        workShopsModel.IsGoing.Add(p.IsGoing);
                        workShopsModel.Id.Add(p.AssignWorkShopId);
                    }
                      
                }
                ViewBag.Entry = HttpUtil.CurrentUser.UserType;
                return View(workShopsModel);
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
            
        }

        public ActionResult Going(int id)
        {
            try
            {
                var result = _assignworkShopService.GetById(id);
                result.Data.IsGoing = "true";
                var result2 = _assignworkShopService.Save(result.Data);
                var result4 = _volunteerService.GetById(result.Data.UserId);
              //  result4.Data.WorkShopStatus = "true";
                var result3 = _volunteerService.Save(result4.Data);
                return RedirectToAction("WorkShopInformation");
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        public ActionResult NotGoing(int id)
        {
            try
            {
                var result = _assignworkShopService.GetById(id);
                result.Data.IsGoing = "not";
                var result2 = _assignworkShopService.Save(result.Data);
                return RedirectToAction("WorkShopInformation");
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        public ActionResult DeleteWorkShop(int id)
        {
            try
            {
                var result = _workshopService.Delete(id);
                var result2 = _assignworkShopService.GetAllByWorkShopId(id);
                foreach (var p in result2.Data)
                {
                    _assignworkShopService.Delete(p.AssignWorkShopId);
                    var result4 = _volunteerService.GetById(p.UserId);
                  //  result4.Data.WorkShopStatus = "false";
                    var result3 = _volunteerService.Save(result4.Data);
                }
                
                return RedirectToAction("GetAllWorkShop");

            }
            catch (Exception e)
            {
                return Content(e.Message);

            }

        }

        public ActionResult FinishWorkShop(int id)
        {
            try
            {
                var result2 = _assignworkShopService.GetById(id);
                var result4 = _volunteerService.GetById(result2.Data.UserId);
               // result4.Data.WorkShopStatus = "false";
                var result3 = _volunteerService.Save(result4.Data);
                var result = _assignworkShopService.Delete(id);

                return RedirectToAction("WorkShopInformation");

            }
            catch (Exception e)
            {
                return Content(e.Message);

            }

        }

        public ActionResult MembersComing(int workShopId)
        {
            try
            {
                var result4 = _workshopService.GetById(workShopId);
                ListViewModel list = new ListViewModel();
                list.WorkShop = result4.Data;
                var result = _assignworkShopService.GetAllByWorkShopId(workShopId);
                var members = result.Data.Where(q => q.IsGoing.Equals("true")).ToList();

                foreach (var p in members)
                {
                    var result2 = _volunteerService.GetById(p.UserId);
                    var result3 = _volunteerDoctorService.GetById(p.UserId);
                    if (result3.Data != null)
                    {
                        var user = _userService.GetById(p.UserId).Data;
                        UserDoctorViewModel uv = new UserDoctorViewModel();
                        uv.UserId = user.UserId;
                        uv.Email = user.Email;
                        uv.Name = user.Name;
                        uv.Phone = user.Phone;
                        uv.SpecialDomain = result3.Data.SpecialDomain;
                        uv.CurrentLocation = result3.Data.CurrentLocation;
                        list.UserDoctorViewModel.Add(uv);
                    }
                    else
                    {
                        var user = _userService.GetById(p.UserId).Data;
                        UserVolunteerViewModel uv = new UserVolunteerViewModel();
                        uv.UserId = user.UserId;
                        uv.Email = user.Email;
                        uv.Name = user.Name;
                        uv.Phone = user.Phone;
                        uv.JobDetails = result2.Data.JobDetails;
                        uv.CurrentLocation = result2.Data.CurrentLocation;
                        list.UserVolunteerViewModels.Add(uv);
                    }

                }
                return View(list);

            }
            catch (Exception e)
            {
                return Content(e.Message);

            }
        }
    }
}