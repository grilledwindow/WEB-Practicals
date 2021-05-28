using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web_S10203108.Models;
using Web_S10203108.DAL;
using System;

namespace Web_S10203108.Controllers
{
    public class StaffController : Controller
    {
        private StaffDAL staffContext = new StaffDAL();
        private BranchDAL branchContext = new BranchDAL();

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

        private List<Branch> GetAllBranches()
        {
            // Get a list of branches from database
            List<Branch> branchList = branchContext.GetAllBranches();

            // Adding a select prompt at the first row of the branch list
            branchList.Insert(0, new Branch
            {
                BranchNo = 0,
                Address = "--Select--"
            });
            return branchList;
        }

        // GET: StaffController/Edit/5
        public ActionResult Edit(int? id)
        {
            // Stop accessing the action if not logged in
            // or account not in the "Staff" role
            if ((HttpContext.Session.GetString("Role") == null) ||
                (HttpContext.Session.GetString("Role") != "Staff"))
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == null)
            {
                // Query string parameter not provided
                // Return to listing page, not allowed to edit
                return RedirectToAction("Index");
            }

            ViewData["BranchList"] = GetAllBranches();

            Staff staff = staffContext.GetDetails(id.Value);
            if (staff == null)
            {
                // Return to listing page, not allowed to edit
                return RedirectToAction("Index");
            }

            return View(staff);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Staff staff)
        {
            // Get branch list for drop-down list
            // in case of the need to return to Edit.cshtml view
            ViewData["BranchList"] = GetAllBranches();
            if (ModelState.IsValid)
            {
                // Update staff record to database
                staffContext.Update(staff);
                return RedirectToAction("Index");
            }
            else
            {
                // Input validation fails, return to the view
                // to display error message
                return View(staff);
            }
        }

        // GET: StaffController/Delete/5
        public ActionResult Delete(int? id)
        {
            // Stop accessing the action if not logged in
            // or account not in the "Staff" role
            if ((HttpContext.Session.GetString("Role") == null) ||
                (HttpContext.Session.GetString("Role") != "Staff"))
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == null)
            {
                // Return to listing page, not allowed to edit
                return RedirectToAction("Index");
            }

            Staff staff = staffContext.GetDetails(id.Value);
            if (staff == null)
            {
                // Return to listing page, not allowed to edit
                return RedirectToAction("Index");
            }
            return View(staff);
        }

        // POST: StaffController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Staff staff)
        {
            // Delete the staff record from database
            staffContext.Delete(staff.StaffId);
            return RedirectToAction("Index");
        }
    }
}