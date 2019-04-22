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
    public class DonationController : Controller
    {
        private IUserService _userService;
        private IDonationService _donationService;

        public DonationController(UserService userService, DonationService donationService)
        {
            _userService = userService;
            _donationService = donationService;
        }

        // GET: Donation
        public ActionResult AddDonation()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddDonation(Donation donation)
        {
            try
            {
                var result = _donationService.Save(donation);
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

        public ActionResult GetAllDonation()
        {
            try
            {
                var result = _donationService.GetAll();
                double totalamount = 0;
                foreach (var v in result.Data)
                {
                    totalamount += v.Amount;
                }
                ViewBag.TotalAmount = totalamount;
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

        public ActionResult DonationDetails(int id)
        {
            try
            {
                var result = _donationService.GetById(id);
                if (result.HasError)
                {
                    ViewBag.Message = result.Message;
                    return Content(result.Message);
                }
                return Content(result.Message);
            }
            catch (Exception e)
            {
                return Content(e.Message);

            }

        }

    }
}