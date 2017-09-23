using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Project.Entities.SiteMap;

namespace Project.Entities.Identity
{
    public class Role : IdentityRole
    {
        public string Description { get; set; }

        public virtual ICollection<SiteMapRole> SiteMapRoles { get; set; }
    }
}
