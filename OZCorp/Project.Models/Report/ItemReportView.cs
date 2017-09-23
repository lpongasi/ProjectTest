using System.Collections.Generic;

namespace Project.Models.Report
{

    public class ItemReportViewResponse
    {
        public IList<ItemReportView> ItemReportViews { get; set; }
        public ItemReportViewTotal ItemReportViewTotal { get; set; }
    }
    public class ItemReportView
    {
        public long Id { get; set; }
        public string Barcode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Size { get; set; }
        public string ImageLocation { get; set; }
        public decimal Price { get; set; }
        public string PriceString => Price.ToString("N2");
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public string TotalString => Total.ToString("N2");
    }

    public class ItemReportViewTotal
    {
        public decimal Total { get; set; }
        public string TotalString => Total.ToString("N2");
    }
}
