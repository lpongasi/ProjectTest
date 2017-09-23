using Project.Common.Extensions;

namespace Project.Models.PurchaseOrder
{
    public class POITemViewModel
    {
        public long PurchaseOrderId { get; set; }
        public long ItemId { get; set; }
        public string Barcode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Qty { get; set; }
        public decimal Discount { get; set; }
        public string Remarks { get; set; }
        public string Size { get; set; }

        public decimal TotalPrice => Price * Qty;
        public decimal PriceDiscount => TotalPrice * Discount;
        public decimal Total => TotalPrice - PriceDiscount;
        public decimal DiscountPercentage => Discount * 100;
        public string DiscountString => Discount.ToPercentage();
        public string PriceDiscountString => PriceDiscount.ToString("N2");
        public string PriceString => Price.ToString("N2");
        public string TotalPriceString => TotalPrice.ToString("N2");
        public string TotalString => Total.ToString("N2");

    }
}
