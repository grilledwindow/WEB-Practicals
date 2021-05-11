using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_S10203108.Models;
namespace web_s1234567.Controllers
{
    public class StaffController : Controller
    {
        private StaffDAL staffContext = new StaffDAL();
        // GET: Staff
        public ActionResult Index()
        {
            // Stop accessing the action if not logged in
            // or account not in the "Staff" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Staff"))
            {
                return RedirectToAction("Index", "Home");
            }
            
            List<Staff> staffList = staffContext.GetAllStaff();
            return View(staffList);
        }
    }
}