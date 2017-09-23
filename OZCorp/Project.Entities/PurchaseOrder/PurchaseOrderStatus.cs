using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Project.Common.Enums;

namespace Project.Entities.PurchaseOrder
{
    [Table("PurchaseOrderStatus")]
    public class PurchaseOrderStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public PoStatus Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ClassIcon { get; set; }
        public string ClassName { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual ICollection<PurchaseOrderAction> PoStatusActions { get; set; }
        public virtual ICollection<PurchaseOrderAction> ValidStatusActions { get; set; }
        public virtual ICollection<PurchaseOrderAction> NextStatusActions { get; set; }
    }
}
