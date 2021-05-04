using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Web_S10203108.Models;

namespace Web_S10203108.Controllers
{
    public class RentalController : Controller
    {
 
        // List that stores the options for days (duration) of loan
        private List<int> numLoanDays = new List<int> { 2, 5, 10, 20 };
        // List that stores the corresponding rental rate for each options
        private List<double> rentalRates = new List<double> { 1.0, 1.50, 2.50, 5.00 };
        // A list for populating drop-down list
        private List<SelectListItem> numBooks = new List<SelectListItem>();
        // A list for populating checkbox list
        private List<RentalDiscount> discountList =
        new List<RentalDiscount>();
        // Constructor for the RentalController
        public RentalController()
        {
            //Populate the selection list for drop-down list
            for (int i = 1; i <= 10; i++)
            {
                numBooks.Add(
                    new SelectListItem
                    {
                        Value = i.ToString(),
                        Text = i.ToString(),
                    }
                );
            }
            //Populate the selection list for checkboxes list
            discountList.Add(
                new RentalDiscount
                {
                    Description = "Student Membership (20%)",
                    DiscountPercent = 20,
                    Selected = false
                }
            );

            discountList.Add(
                new RentalDiscount
                {
                    Description = "Birthday Discount (10%)",
                    DiscountPercent = 10,
                    Selected = false
                }
            );
        }

        public ActionResult Calculate()
        {
            // Stop accessing the action if not logged in
            // or account not in the "Staff" role
            if ((HttpContext.Session.GetString("Role") == null) ||
                (HttpContext.Session.GetString("Role") != "Staff"))
            {
                return RedirectToAction("Index", "Home");
            }

            //Prepare the ViewData to be used in Calculate.cshtml view
            ViewData["ShowResult"] = false;
            ViewData["NumBooks"] = numBooks;
            ViewData["NumDays"] = numLoanDays;
            Rental rental = new Rental
            {
                LoanDate = DateTime.Today,
                // Set the default number of books loaned
                NumBooks = Convert.ToInt32(numBooks[0].Value),
                // Set the default number of days loaned
                NumDays = numLoanDays[0],
                // Display a list of discount
                Discounts = discountList
            };

            return View(rental);
        }

        [HttpPost]
        public ActionResult Calculate(Rental rental)
        {

            //Prepare the ViewData to be used in Calculate.cshtml view
            ViewData["ShowResult"] = true;
            ViewData["NumBooks"] = numBooks;
            ViewData["NumDays"] = numLoanDays;

            // rental object contains user input in the Calculate.cshtml view
            // Compute Loan Due Date
            rental.DueDate = rental.LoanDate.AddDays(rental.NumDays);

            // Get rental rate based on number of day loan selection
            int selectedIndex = numLoanDays.IndexOf(rental.NumDays);
            rental.RentalRate = rentalRates[selectedIndex];

            // Calculate rental fee
            rental.RentalFee = rental.NumBooks * rental.NumDays * rental.RentalRate;

            // Calculate the discount percentage based on rental fee
            rental.DiscountPercent = 0.0;
            foreach (RentalDiscount discountItem in rental.Discounts)
            {
                if (discountItem.Selected)
                    rental.DiscountPercent += discountItem.DiscountPercent;
            }

            // Calculate the amount payable
            rental.AmountPayable = rental.RentalFee * (100 - rental.DiscountPercent) / 100;

            // Route to Calculate.cshtml view to display result
            // contained in the rental object
            return View(rental);
        }
    }
}