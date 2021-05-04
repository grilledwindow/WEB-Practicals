using System;
using Microsoft.AspNetCore.Mvc;
using Web_S10203108.Models;

namespace Web_S10203108.Controllers
{
    public class FineController : Controller
    {
        public ActionResult Calculate()
        {
            //Prepare the ViewData to be use in Calculate.cshtml view
            ViewData["ShowResult"] = false;
            Fine fine = new Fine();
            fine.DueDate = DateTime.Today;
            fine.FineRate = 0.50;
            return View(fine);
        }

        [HttpPost]
        public ActionResult Calculate(Fine fine)
        {
            // The fine object contains user inputs from view
            if (!ModelState.IsValid) // validation fails
            {
                Console.WriteLine("yes");
                return View(fine); // returns the view with errors
            }

            // Calculate the cumulative fine and its breakdown
            double fineTotal = 0.0;
            string fineBreakdown = "";

            for (int count = 1; count <= fine.NumBooksOverdue; count++)
            {
                double fineForEachBook = count * fine.FineRate * fine.NumDaysOverdue;
                fineTotal += fineForEachBook;
                fineBreakdown += $"Overdue cost for Book {count} = ${fineForEachBook:#,##0.00}<br />";
            }
            fine.FineAmt = fineTotal;

            // Prepare the ViewData to be used in Calculate.cshtml view
            ViewData["ShowResult"] = true;
            ViewData["FineBreakdown"] = fineBreakdown;

            // Route to Calculate.cshtml view to display result
            return View(fine);
        }
    }
}