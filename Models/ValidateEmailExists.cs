using System;
using System.ComponentModel.DataAnnotations;

namespace Web_S10203108.Models
{
    public class ValidateEmailExists : ValidationAttribute
    {
        private StaffDAL staffContext = new StaffDAL();
        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            // Get the email value to validate
            string email = Convert.ToString(value);
            
            // Casting the validation context to the "Staff" model class
            Staff staff = (Staff)validationContext.ObjectInstance;
            
            // Get the Staff Id from the staff instance
            int staffId = staff.StaffId;

            return staffContext.IsEmailExist(email, staffId) 
                ? new ValidationResult ("Email address already exists!")
                : ValidationResult.Success;
        }
    }
}
