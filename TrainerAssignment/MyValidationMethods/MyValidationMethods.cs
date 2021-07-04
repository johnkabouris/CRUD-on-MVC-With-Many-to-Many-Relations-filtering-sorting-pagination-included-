using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrainerAssignment.MyValidationMethods
{
    public class MyValidationMethods
    {
        public static ValidationResult ValidateSalary(double value,ValidationContext context)
        {
            bool isValid = true;
            if (value <= 0)
            {
                isValid = false;
            }
            if (isValid)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(string.Format("The field{0} must be greater or equal to zero", context.MemberName), new List<string> { context.MemberName });
            }
        }
        public static ValidationResult FirstLetterCapital(string input,ValidationContext context)
        {
            
            if (input.Length == 0)
            {
                Console.WriteLine("Field cannot be empty");
            }
            if(input.Length>=1 && char.IsUpper(input[0]))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(string.Format("First letter must be capital ", context.MemberName), new List<string> { context.MemberName });
            }
            
            
        }
    }
    
}