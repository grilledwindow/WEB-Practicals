using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_S10203108.DAL;
using Web_S10203108.Models;
namespace web_s1234567.Controllers
{
    public class BranchController : Controller
    {
        private BranchDAL branchContext = new BranchDAL();
        // GET: BranchController
        public ActionResult Index(int? id)
        {
            // Stop accessing the action if not logged in
            // or account not in the "Staff" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Staff"))
            {
                return RedirectToAction("Index", "Home");
            }
            BranchViewModel branchVM = new BranchViewModel();
            branchVM.branchList = branchContext.GetAllBranches();
            // Check if BranchNo (id) presents in the query string
            if (id != null)
            {
                ViewData["selectedBranchNo"] = id.Value;
                // Get list of staff working in the branch
                branchVM.staffList = branchContext.GetBranchStaff(id.Value);
            }
            else
            {
                ViewData["selectedBranchNo"] = "";
            }
            return View(branchVM);
        }
    }
}