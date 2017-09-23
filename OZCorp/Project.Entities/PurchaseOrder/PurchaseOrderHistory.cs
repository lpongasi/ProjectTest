using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Project.Entities.User;

namespace Project.Entities.PurchaseOrder
{
    [Table("PurchaseOrderHistory")]
    public class PurchaseOrderHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string ActionUserId { get; set; }
        public string Action { get; set; }
        public DateTime ActionDate { get; set; }
        public long PoId { get; set; }

        [ForeignKey("PoId")]
        public virtual PurchaseOrder PurchaseOrder { get; set; }
        [ForeignKey("ActionUserId")]
        public virtual UserInfo ActionUser { get; set; }
    }
}
