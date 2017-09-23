using System.Collections.Generic;
using System.Linq;
using Project.Common.Extensions;
using Project.Entities.Product;

namespace Project.Models.Item
{
    public class ItemViewModel
    {
        public long Id { get; set; }
        public string Barcode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitCost { get; set; }
        public decimal Price { get; set; }
        public int Qty { get; set; }
        public int QtyNotification { get; set; }
        public string StockNo { get; set; }
        public string PartNo { get; set; }
        public string Location { get; set; }
        public bool PurPro { get; set; }
        public string PurProString => PurPro.ToStringBoolean();
        public bool NotForSale { get; set; }
        public string NotForSaleString => NotForSale.ToStringBoolean();
        public bool IsPrivate { get; set; }
        public string IsPrivateString => IsPrivate.ToStringBoolean();
        public bool Removed { get; set; }
        public string Uom { get; set; }
        public string PriceString => Price.ToString("N2");
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Size { get; set; }
        public IList<string> ImageLocation => Images.Select(si => si.FileLocation).ToList();
        public ICollection<ImageLocation> Images { get; set; }
        public ICollection<ItemHistory> ItemHistories { get; set; }
    }
}
