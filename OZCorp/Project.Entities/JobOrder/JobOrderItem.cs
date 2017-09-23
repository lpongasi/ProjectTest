using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Project.Common.Enums;

namespace Project.Entities.JobOrder
{
    [Table("JobOrderItem")]
    public class JobOrderItem
    {
        [Key, Column(Order = 0)]
        public long JobOrderId { get; set; }
        [Key, Column(Order = 1)]
        public long ItemId { get; set; }
        public int Qty { get; set; }
        public int RejectQty { get; set; }

        public int EstDay { get; set; }
        public int EstHour { get; set; }
        public int EstMinute { get; set; }

        public DateTime? DateTimeStart { get; set; }
        public DateTime? DateTimeEnd { get; set; }
        
        public JoItemStatus StatusId { get; set; }
        [ForeignKey("StatusId")]
        public virtual JobOrderItemStatus Status { get; set; }

        public string Remarks { get; set; }

        [ForeignKey("JobOrderId")]
        public virtual JobOrder JobOrder { get; set; }
        [ForeignKey("ItemId")]
        public virtual Product.Item Item { get; set; }
    }
}
