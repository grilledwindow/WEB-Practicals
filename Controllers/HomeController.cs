using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Google.Apis.Auth;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Web_S10203108.Models;
using static Google.Apis.Auth.GoogleJsonWebSignature;

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
                HttpContext.Session.SetString("LoginTime", DateTime.Now.ToString()); // dd-MMM-yy HH:mm:ss tt
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

        [Authorize]
        public async Task<ActionResult> StudentLogin()
        {
            // The user is already authenticated, so this call won't
            // trigger login, but it allows us to access token related values.
            AuthenticateResult auth = await HttpContext.AuthenticateAsync();
            string idToken = auth.Properties.GetTokenValue(OpenIdConnectParameterNames.IdToken);
            try
            {
                // Verify the current user logging in with Google server
                // if the ID is invalid, an exception is thrown
                Payload currentUser = await
                GoogleJsonWebSignature.ValidateAsync(idToken);
                string userName = currentUser.Name;
                string eMail = currentUser.Email;
                HttpContext.Session.SetString("LoginID", userName + " / " + eMail);
                HttpContext.Session.SetString("Role", "Student");
                HttpContext.Session.SetString("LoginTime", DateTime.Now.ToString()); // dd-MMM-yy HH:mm:ss tt
                return RedirectToAction("Index", "Book");
            }
            catch (Exception e)
            {
                // Token ID is may be tempered with, force user to logout
                return RedirectToAction("LogOut");
            }
        }

        public async Task<ActionResult> LogOut()
        {
            // Clear authentication cookie
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
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
