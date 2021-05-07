using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Web_S10203108.Models;

namespace Web_S10203108.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public ActionResult StaffLogin(IFormCollection formData)
        {
            // Read inputs from textboxes
            // Email address converted to lowercase
            string loginID = formData["txtLoginID"].ToString().ToLower();
            string password = formData["txtPassword"].ToString();

            if (loginID == "abc@npbook.com" && password == "pass1234")
            {
                String loginTime = DateTime.Now.ToString("dd-MMM-yy HH:mm:ss tt");
                TempData["LoginTime"] = loginTime;
                HttpContext.Session.SetString("LoginTime", loginTime);
                HttpContext.Session.SetString("LoginID", loginID);
                HttpContext.Session.SetString("Role", "Staff");

                // Redirect user to the "StaffMain" view through an action
                return RedirectToAction("StaffMain");
            }
            else
            {
                // Temp data is used to pass info from one action to another
                TempData["Message"] = "Invalid Login Credentials!";

                // Redirect user back to the index view through an action
                return RedirectToAction("Index");
            }
        }

        public ActionResult LogOut()
        {
            DateTime loginTime = DateTime.Parse(HttpContext.Session.GetString("LoginTime").ToString());

            // Clear all key-values pairs stored in session state
            HttpContext.Session.Clear();
            TempData["LoginDuration"] = ((DateTime.Now - loginTime).TotalSeconds).ToString();

            // Call the Index action of Home controller
            return RedirectToAction("Index");
        }
        public ActionResult StaffMain()
        {
            return View();
        }
    };
}
