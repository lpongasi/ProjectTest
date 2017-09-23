using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Project.Entities.User;
using Project.Common.Enums;
using System;

namespace Project.Entities.JobOrder
{
    [Table("JobOrder")]
    public class JobOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string UserId { get; set; }
        public DateTime JoDate { get; set; }
        public JoStatus JobOrderStatusId { get; set; }
        public string Remarks { get; set; }



        public virtual ICollection<JobOrderHistory> History { get; set; }
        public virtual ICollection<JobOrderItem> JobOrderItems { get; set; }
        [ForeignKey("JobOrderStatusId")]
        public virtual JobOrderStatus JobOrderStatus { get; set; }
        [ForeignKey("UserId")]
        public virtual UserInfo User { get; set; }

        public long? PurchaseOrderId { get; set; }
        [ForeignKey("PurchaseOrderId")]
        public virtual PurchaseOrder.PurchaseOrder PurchaseOrder { get; set; }

    }
}
