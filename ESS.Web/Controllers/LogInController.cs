using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using App.Framework;
using ESS.Core.Entity;
using ESS.Core.Service;
using ESS.Service.Interface;
using Newtonsoft.Json;

namespace ESS.Web.Controllers
{
    public class LogInController : Controller
    {
        private IAuthenticationService _service;
        private IVolunteerService _volunteerService;
        private IVolunteerDoctorService _volunteerDoctorService;
        private IUserService _userService;

        public LogInController(IAuthenticationService service ,
            VolunteerService volunteerService, UserService userService, VolunteerDoctorService volunteerDoctorService)
        {
            _service = service;
            _volunteerService = volunteerService;
            _volunteerDoctorService = volunteerDoctorService;
            _userService = userService;
        }

        // GET: LogIn
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(User user)
        {
            try
            {
                var obj= _service.GetByEmail(user.Email);
                var volunteer = _volunteerService.GetById(obj.Data.UserId);

                if (obj.Data.UserType.Equals("Volunteer"))
                {
                    if (volunteer.Data.IsApprove.Equals("false"))
                    {
                        return Content("Your Volunteer Request is Pending !!! Please wait");

                    }
                    if (volunteer.Data.IsVolunteerDone.Equals("Done"))
                    {
                        volunteer.Data.IsActive = "true";
                    }
                    else
                    {
                        volunteer.Data.IsActive = "false";

                    }

                    _volunteerService.Save(volunteer.Data);
                }
                var volunteerdoctor = _volunteerDoctorService.GetById(obj.Data.UserId);

                if (obj.Data.UserType.Equals("Doctor"))
                {
                    if (volunteerdoctor.Data.IsDoctorDone.Equals("Done"))
                    {
                        volunteerdoctor.Data.IsActive = "true";
                    }
                    else
                    {
                        volunteerdoctor.Data.IsActive = "false";

                    }
                    _volunteerDoctorService.Save(volunteerdoctor.Data);
                }
                var result = _service.Login(user.Email, user.Password);
                if (result == false)
                {
                    ViewBag.Message = "Invalid Email and Password";
                    return Content("Invalid Email and Password");
                }
                if (obj.HasError)
                {
                    ViewBag.Message = "Invalid Email and Password";
                    return Content(obj.Message);
                }
                var jasonUserInfo = JsonConvert.SerializeObject(obj.Data);
                FormsAuthentication.SetAuthCookie(jasonUserInfo, false);
                if (obj.Data.UserType.Equals("Volunteer"))
                {
                    return RedirectToAction("VolunteerActivities", "Volunteer");

                }
               else if (obj.Data.UserType.Equals("Doctor"))
                {
                    return RedirectToAction("VolunteerDoctorActivities", "VolunteerDoctor");

                }
                return RedirectToAction("GetAllRequest", "Request");
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
           
        }

        public ActionResult Logout()
        {
            
            try
            {
                var result = _userService.GetById(HttpUtil.CurrentUser.UserId);
                if (result.Data.UserType.Equals("Volunteer"))
                {
                    var volunteer = _volunteerService.GetById(HttpUtil.CurrentUser.UserId);
                    volunteer.Data.IsActive = "false";
                    _volunteerService.Save(volunteer.Data);

                }
                if (result.Data.UserType.Equals("Doctor"))
                {
                    var volunteerDoctor = _volunteerDoctorService.GetById(HttpUtil.CurrentUser.UserId);
                    volunteerDoctor.Data.IsActive = "false";
                    _volunteerDoctorService.Save(volunteerDoctor.Data);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }


    }
}