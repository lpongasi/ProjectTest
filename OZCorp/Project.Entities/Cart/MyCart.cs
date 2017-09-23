using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Project.Entities.User;

namespace Project.Entities.Cart
{
    [Table("MyCart")]
    public class MyCart
    {
        [Key, Column(Order = 0)]
        public string UserId { get; set; }
        [Key, Column(Order = 1)]
        public long ItemId { get; set; }
        public int Quantity { get; set; }
        public bool Selected { get; set; }

        [ForeignKey("UserId")]
        public UserInfo User { get; set; }

        [ForeignKey("ItemId")]
        public Product.Item Item { get; set; }
    }
}
