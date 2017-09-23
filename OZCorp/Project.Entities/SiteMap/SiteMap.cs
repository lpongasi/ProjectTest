using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Entities.SiteMap
{
    [Table("SiteMap")]
    public class SiteMap
    {

        [Key]
        public string Id { get; set; }
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Method { get; set; }


        public string Title { get; set; }
        public string Description { get; set; }


        public string ClassIcon { get; set; }
        public string ClassName { get; set; }

        public string ParentId { get; set; }
        public bool IsPublic { get; set; }
        public bool IsVisible { get; set; }
        public bool IsActive { get; set; }
        public int Sequence { get; set; }

        public virtual ICollection<SiteMapRole> SiteMapRoles { get; set; }
    }
}
