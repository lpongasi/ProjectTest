using System.Collections.Generic;

namespace Project.Models.Cart
{
    public class MyCartViewModel
    {
        public long ItemId { get; set; }
        public int Quantity { get; set; }
        public int QuantityLeft { get; set; }
        public string Barcode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Total => Price * Quantity;
        public string UnitOfMeasure { get; set; }
        public bool Selected { get; set; }
        public bool ItemRemoved { get; set; }
        public bool ItemNotForSale { get; set; }
        public bool PurPro { get; set; }
        public IEnumerable<string> ImageLocation { get; set; }
        public string PriceString => Price.ToString("N2");
        public string TotalString => Total.ToString("N2");
        public bool Available =>   !ItemNotForSale && !ItemRemoved && (QuantityLeft >= Quantity || PurPro);
        public string Status =>   ItemNotForSale || ItemRemoved
                                     ? "Not Available"
                                     : (QuantityLeft >= Quantity || PurPro)
                                     ? "Available"
                                     : "Ordered Quantity is too high!";

    }
}
