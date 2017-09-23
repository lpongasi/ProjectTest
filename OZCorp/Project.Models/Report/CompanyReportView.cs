using System.Collections.Generic;
using Project.Common.Extensions;

namespace Project.Models.Report
{

    public class CompanyReportViewResponse
    {
        public IList<CompanyReportView> CompanyReportViews { get; set; }
        public CompanyReportViewTotal CompanyReportViewTotal { get; set; }
    }
    public class CompanyReportView
    {
        public string CompanyName { get; set; }
        public string CompanyContact { get; set; }
        public string CompanyAddress { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public string StringDiscount => Discount.ToPercentage();
        public string StringTotal => Total.ToMoney();
    }
    public class CompanyReportViewTotal
    {
        public decimal Total { get; set; }
        public string TotalString => Total.ToString("N2");
    }
}
