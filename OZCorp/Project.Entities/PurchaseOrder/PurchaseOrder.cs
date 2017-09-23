using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Project.Common.Enums;
using Project.Entities.User;
using System;

namespace Project.Entities.PurchaseOrder
{
    [Table("PurchaseOrder")]
    public class PurchaseOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string UserId { get; set; }
        public PoStatus PurchaseOrderStatusId { get; set; }
        public decimal Discount { get; set; }
        public decimal Tax { get; set; }
        public string OtherRemarks { get; set; }
        public decimal OtherFees { get; set; }
        public string Remarks { get; set; }
        public DateTime PoDate { get; set; }
        public virtual ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; }
        public virtual ICollection<PurchaseOrderHistory> History { get; set; }

        [ForeignKey("PurchaseOrderStatusId")]
        public virtual PurchaseOrderStatus PurchaseOrderStatus { get; set; }
        [ForeignKey("UserId")]
        public virtual UserInfo User { get; set; }
    }
}
