using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web_S10203108.Models
{
    public class Branch
    {
        [Display(Name = "ID")]
        public int BranchNo { get; set; }

        public string Address { get; set; }

        [RegularExpression(@"[689]\d{7}|\+65[689]\d{7}$",
            ErrorMessage = "Invalid Singapore Phone Number")]
        public string Telephone { get; set; }
    }
}