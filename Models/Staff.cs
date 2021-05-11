using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web_S10203108.Models
{
    public class Staff
    {
        [Display(Name = "ID")]
        public int StaffId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
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
        [Range(1.00, 10000.00, ErrorMessage = "Salary must be range from 1.00 to 10000.00")]
        public decimal Salary { get; set; }

        [Display(Name = "Full-Time Staff")]
        public bool IsFullTime { get; set; }

        [Display(Name = "Branch")]
        public int? BranchNo { get; set; }
    }
}