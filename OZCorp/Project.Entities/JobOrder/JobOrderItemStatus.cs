using Project.Common.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Entities.JobOrder
{
    [Table("JobOrderItemStatus")]
    public class JobOrderItemStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public JoItemStatus Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ClassIcon { get; set; }
        public string ClassName { get; set; }


        public virtual ICollection<JobOrderItem> JobOrderItems { get; set; }
        public virtual ICollection<JobOrderItemAction> StatusActions { get; set; }
        public virtual ICollection<JobOrderItemAction> ValidStatusActions { get; set; }
        public virtual ICollection<JobOrderItemAction> NextStatusActions { get; set; }
    }
}
