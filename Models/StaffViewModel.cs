using System;
using System.ComponentModel.DataAnnotations;

namespace Web_S10203108.Models
{
    public class StaffViewModel
    {
        [Display(Name = "ID")]
        public int StaffId { get; set; }
        public string Name { get; set; }
        public char Gender { get; set; }
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime? DOB { get; set; }
        public string Nationality { get; set; }
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        [Display(Name = "Monthly Salary (SGD)")]
        [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
        public decimal Salary { get; set; }
        public string Status { get; set; }
        [Display(Name = "Branch")]
        public string BranchName { get; set; }
        public string Photo { get; set; }
    }
}