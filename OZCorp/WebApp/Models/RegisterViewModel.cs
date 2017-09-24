using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Project.Common.Attributes;

namespace Project.Models.Account
{
    public class RegisterViewModel
    {
        [Required, EmailAddress, Display(Name = "Email"), RemoteEmailCheck]
        public string Email { get; set; }
        [Display(Name = "Password"),
            DataType(DataType.Password),
            StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6),
            RegularExpression(@"(?=.*\d)(?=.*\W+)(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$",ErrorMessage = "Passwords will contain at least 1 upper case letter, 1 lower case letter,1 number and 1 special character."),
            Required]
        public string Password { get; set; }
        [Display(Name = "Confirm password"), 
            DataType(DataType.Password),
            Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Employee ID or User ID (not required)")]
        public string UserId { get; set; }
        [Required, Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required, Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }
        [Required, Display(Name = "Name")]
        public string CompanyName { get; set; }
        [Required, Display(Name = "Contact Number")]
        public string CompanyContact { get; set; }
        [Required, Display(Name = "Address")]
        public string CompanyAddress { get; set; }
        [Required, Range(0, 100), Display(Name = "Discount(%)")]
        public int Discount { get; set; }
        [Required, Range(0, 100), Display(Name = "Tax(%)")]
        public int Tax { get; set; }
        [Display(Name = "Other fee Remarks")]
        public string OtherRemarks { get; set; }
        [Display(Name = "Other fee Ammount")]
        public decimal OtherFees { get; set; }
        [Display(Name = "User Role")]
        public IEnumerable<string> Roles { get; set; }
    }
}
