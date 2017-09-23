using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Project.Entities.Cart;
using Project.Entities.Category;
using Project.Entities.PurchaseOrder;

namespace Project.Entities.Product
{
    [Table("Item")]
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required, Display(Name = "Barcode")]
        public string Barcode { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required, Display(Name = "Unit Cost")]
        public decimal UnitCost { get; set; }
        [Required, Display(Name = "Price")]
        public decimal Price { get; set; }
        [Required, Display(Name = "Quantity")]
        public int Qty { get; set; }
        [Required, Display(Name = "Notify Quantity")]
        public int QtyNotification { get; set; }

        [Required, Display(Name = "Stock#")]
        public string StockNo { get; set; }
        [Required, Display(Name = "Part#")]
        public string PartNo { get; set; }
        public string Location { get; set; }

        [Display(Name = "Enable => Customer can purchase even out of stocks?")]
        public bool PurPro { get; set; }

        [Display(Name = "Not For Sale")]
        public bool NotForSale { get; set; }

        [Display(Name = "Exclusive Item")]
        public bool IsPrivate { get; set; }

        public bool Removed { get; set; }

        [Display(Name = "Unit of Measure")]
        public int UomId { get; set; }
        [ForeignKey("UomId")]
        public virtual UnitOfMeasure UnitOfMeasure { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category.Category Category { get; set; }

        public int SubCategoryId { get; set; }
        [ForeignKey("SubCategoryId")]
        public virtual SubCategory SubCategory { get; set; }


        [Display(Name = "Size")]
        public int? SizeId { get; set; }
        [ForeignKey("SizeId")]
        public virtual Size Size { get; set; }

        public virtual ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; }
        public virtual ICollection<MyCart> MyCarts { get; set; }
        public virtual ICollection<ImageLocation> Images { get; set; }
        public virtual ICollection<ItemHistory> ItemHistories { get; set; }

    }
}
