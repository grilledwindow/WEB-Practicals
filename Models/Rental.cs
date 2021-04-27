using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web_S10203108.Models
{
    public class RentalDiscount
    {
        public string Description { get; set; }
        public double DiscountPercent { get; set; }
        public bool Selected { get; set; }
    }
    public class Rental
    {
        [Display(Name = "Loan Date")]
        [DataType(DataType.Date)]

        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime LoanDate { get; set; }
        [Display(Name = "Number of Books")]
        public int NumBooks { get; set; }

        // To do . . .
        // Define the property for NumDays. Set the display label
        // to "Number of Days"
        [Display(Name = "Number of Days")]
        public int NumDays { get; set; }

        public double RentalRate { get; set; }
        
        [Display(Name = "Due Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime DueDate { get; set; }

        [Display(Name = "Rental Fee (SGD)")]
        [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
        public double RentalFee { get; set; }

        [Display(Name = "Discount Given (%)")]
        [DisplayFormat(DataFormatString = "{0:#0.0}")]
        public double DiscountPercent { get; set; }

        [Display(Name = "Discount Given (%)")]
        public List<RentalDiscount> Discounts { get; set; }

        // To do . . .
        // Define the property for AmountPayable. Set the display label
        // to "Amount Payable (SGD)" and the display format to 2 decimal
        // places and comma at the thousand mark
        [Display(Name = "Amount Payable (SGD)")]
        [DisplayFormat(DataFormatString = "{0:#,0.00}")]
        public double AmountPayable { get; set; }
    }
}