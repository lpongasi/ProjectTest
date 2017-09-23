using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Project.Entities.Identity
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string CompanyContact { get; set; }
        [Required]
        public string CompanyAddress { get; set; }
        public decimal Discount { get; set; }
        public decimal Tax { get; set; }
        public string OtherRemarks { get; set; }
        public decimal OtherFees { get; set; }
        public bool? IsRemove { get; set; }
        public string FullName => $"{LastName}, {FirstName}";
    }
}
