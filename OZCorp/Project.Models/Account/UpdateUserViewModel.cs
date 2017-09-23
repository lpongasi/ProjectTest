using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Models.Account
{
    public class UpdateUserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
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
        public decimal Discount { get; set; }
        [Required, Range(0, 100), Display(Name = "Tax(%)")]
        public decimal Tax { get; set; }
        [Display(Name = "Other fee Remarks")]
        public string OtherRemarks { get; set; }
        [Display(Name = "Other fee Ammount")]
        public decimal OtherFees { get; set; }
        [Display(Name = "User Role")]
        public IEnumerable<string> Roles { get; set; }
    }
}
