using System;
using System.Collections.Generic;
using System.Linq;
using Project.Common.Enums;
using Project.Common.Extensions;

namespace Project.Models.PurchaseOrder
{
    public class POViewModel
    {
        public POViewModel()
        {
            Items = new List<POITemViewModel>();
            Actions = new List<POActionViewModel>();
        }
        public long Id { get; set; }
        public string IdString => $"PO{Id}";
        public decimal Discount { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal Tax { get; set; }
        public string OtherRemarks { get; set; }
        public decimal OtherFees { get; set; }
        public string Remarks { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string CompanyContact { get; set; }
        public string CompanyAddress { get; set; }
        public string FullName => $"{LastName}, {FirstName}";
        public string PoStatus { get; set; }
        public PoStatus PoStatusId { get; set; }
        public IEnumerable<POITemViewModel> Items { get; set; }
        public IEnumerable<POActionViewModel> Actions { get; set; }

        public decimal Total => Items!=null || Items.Any()
                                ? Items.Sum(s => s.Total)
                                : 0;

        public decimal PriceDiscount => Total * Discount;
        public decimal PriceTax => Total * Tax;

        public decimal GrandTotal => (Total + PriceTax + OtherFees) - PriceDiscount;

        public decimal DiscountPercentage => Discount * 100;
        public decimal TaxPercentage => Tax * 100;

        public string OtherFeesString => OtherFees.ToString("N2");
        public string TaxString => Tax.ToPercentage();
        public string DiscountString => Discount.ToPercentage();
        public string TotalString => Total.ToString("N2");
        public string PriceDiscountString => PriceDiscount.ToString("N2");
        public string PriceTaxString => PriceTax.ToString("N2");
        public string GrandTotalString => GrandTotal.ToString("N2");
        public string PurchaseDateString => PurchaseDate.ToString("MMM dd, yyyy");
        public bool Printable { get; set; }
        public bool Editable { get; set; }


        public string MainCompanyName { get; set; }
        public string MainCompanyContact { get; set; }
        public string MainCompanyAddress { get; set; }
    }
}
