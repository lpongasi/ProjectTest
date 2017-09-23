using Project.Common.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Entities.JobOrder
{
    [Table("JobOrderStatus")]
    public class JobOrderStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public JoStatus Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ClassIcon { get; set; }
        public string ClassName { get; set; }
        public virtual ICollection<JobOrder> JobOrders { get; set; }
        public virtual ICollection<JobOrderAction> StatusActions { get; set; }
        public virtual ICollection<JobOrderAction> ValidStatusActions { get; set; }
        public virtual ICollection<JobOrderAction> NextStatusActions { get; set; }
    }
}
