using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Project.Common.Enums;

namespace Project.Entities.PurchaseOrder
{
    [Table("PurchaseOrderAction")]
    public class PurchaseOrderAction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public PoStatus PoStatusId { get; set; }
        public PoStatus ValidStatusId { get; set; }
        public PoStatus NextStatusId { get; set; }

        [ForeignKey("PoStatusId")]
        public virtual PurchaseOrderStatus PoStatus { get; set; }
        [ForeignKey("ValidStatusId")]
        public virtual PurchaseOrderStatus ValidStatus { get; set; }
        [ForeignKey("NextStatusId")]
        public virtual PurchaseOrderStatus NextStatus { get; set; }

    }
}
