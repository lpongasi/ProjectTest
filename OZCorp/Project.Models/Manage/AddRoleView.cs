using System.ComponentModel.DataAnnotations;

namespace Project.Models.Manage
{
    public class AddRoleView
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
