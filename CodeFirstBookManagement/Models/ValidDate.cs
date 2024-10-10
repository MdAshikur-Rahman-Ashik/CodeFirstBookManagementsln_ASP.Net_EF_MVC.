using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CodeFirstBookManagement.Models
{
    public class ValidDate: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (Convert.ToDateTime(value) > DateTime.Now)
            {
                return ValidationResult.Success;
            }
            else
            {
               return new ValidationResult("Current date can not less than Past date");
                
            }
            
        }
    }
}