using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Entities.Product
{
    [Table("ImageLocation")]
    public class ImageLocation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string FileName { get; set; }
        public string FileLocation { get; set; }

        public long? ItemId { get; set; }
        [ForeignKey("ItemId")]
        public virtual Product.Item Item { get; set; }

    }
}
