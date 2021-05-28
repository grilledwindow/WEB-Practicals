using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web_S10203108.Models;
namespace Web_S10203108.Controllers
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

        // GET: Staff/Create
        public ActionResult Create()
        {
            // Stop accessing the action if not logged in
            // or account not in the "Staff" role
            if ((HttpContext.Session.GetString("Role") == null) ||
                (HttpContext.Session.GetString("Role") != "Staff"))
            {
                return RedirectToAction("Index", "Home");
            }

            ViewData["CountryList"] = GetCountries();
            return View();
        }

        private List<SelectListItem> GetCountries()
        {
            List<SelectListItem> countries = new List<SelectListItem>();
            countries.Add(new SelectListItem { Value = "Singapore", Text = "Singapore" });
            countries.Add(new SelectListItem { Value = "Malaysia", Text = "Malaysia" });
            countries.Add(new SelectListItem { Value = "Indonesia", Text = "Indonesia" });
            countries.Add(new SelectListItem { Value = "China", Text = "China" });
            return countries;
        }

        // POST: Staff/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Staff staff)
        {
            // Get country list for drop-down list in case of the need to return to Create.cshtml view
            ViewData["CountryList"] = GetCountries();
            if (ModelState.IsValid)
            {
                // Add staff record to database
                staff.StaffId = staffContext.Add(staff);
                
                // Redirect user to Staff/Index view
                return RedirectToAction("Index");
            }
            else
            {
                // Input validation fails, return to the Create view
                // to display error message
                return View(staff);
            }
        }
    }
}