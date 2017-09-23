using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Project.Entities.Identity;

namespace Project.Entities.SiteMap
{
    [Table("SiteMapRole")]
    public class SiteMapRole
    {
        [Key, Column(Order = 1)]
        public string SiteMapId { get; set; }
        [Key, Column(Order = 2)]
        public string RoleId { get; set; }
        [ForeignKey("SiteMapId")]
        public virtual SiteMap SiteMap { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }
}
