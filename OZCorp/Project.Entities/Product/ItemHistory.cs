using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Project.Entities.User;

namespace Project.Entities.Product
{
    [Table("ItemHistory")]
    public class ItemHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string ActionUserId { get; set; }
        public string Action { get; set; }
        public DateTime ActionDate { get; set; }
        public long ItemId { get; set; }

        [ForeignKey("ItemId")]
        public virtual Product.Item Item { get; set; }
        [ForeignKey("ActionUserId")]
        public virtual UserInfo ActionUser { get; set; }
    }
}
