using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Entities.PurchaseOrder
{
    [Table("PurchaseOrderItem")]
    public class PurchaseOrderItem
    {
        [Key, Column(Order = 0)]
        public long PurchaseOrderId { get; set; }
        [Key, Column(Order = 1)]
        public long ItemId { get; set; }
        public decimal UnitCost { get; set; }
        public decimal Price { get; set; }
        public int Qty { get; set; }
        public int JobQty { get; set; }
        public decimal Discount { get; set; }
        public string Remarks { get; set; }
        [ForeignKey("PurchaseOrderId")]
        public virtual PurchaseOrder PurchaseOrder { get; set; }
        [ForeignKey("ItemId")]
        public virtual Product.Item Item { get; set; }
    }
}
