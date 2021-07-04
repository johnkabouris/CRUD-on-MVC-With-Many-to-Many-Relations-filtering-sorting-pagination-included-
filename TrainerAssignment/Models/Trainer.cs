using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace TrainerAssignment.Models
{
    public class Trainer
    {
        public int TrainerId { get; set; }
        [Required(ErrorMessage ="This Field Cannot be empty"),MaxLength(20),MinLength(3)]
        [Display(Name ="First Name")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "only alphabet")]
        [CustomValidation(typeof(MyValidationMethods.MyValidationMethods),"FirstLetterCapital")]
       
        public string FirstName { get; set; }
        [CustomValidation(typeof(MyValidationMethods.MyValidationMethods), "FirstLetterCapital")]
        
        [Required(ErrorMessage = "This Field Cannot be empty"), MaxLength(20), MinLength(3)]
        [Display(Name = "Last Name")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only alphabet")]
        public string LastName { get; set; }
        
        [CustomValidation(typeof(MyValidationMethods.MyValidationMethods), "ValidateSalary")]
        public decimal Salary { get; set; }
        [Required(ErrorMessage ="This field Cannot be empty")]
        [Display(Name ="DateHire")]
        [DisplayFormat(DataFormatString ="{0:MM/dd/yyyy}")]
        public DateTime DateHire { get; set; }
        [Required(ErrorMessage = "This field Cannot be empty")]
        [Display(Name ="Is Available")]
        public bool IsAvailable { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
}