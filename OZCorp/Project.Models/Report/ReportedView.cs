using System;
using System.Collections.Generic;

namespace Project.Models.Report
{
    public class ReportedView
    {
        public string Action { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public IList<ReportedViewList> Data { get; set; }
    }
    public class ReportedViewUpdate
    {
        public long Id { get; set; }
        public string Action { get; set; }
        public IList<ReportedViewList> Data { get; set; }
    }
    public class ReportedViewList
    {
        public string Title { get; set; }
        public int Name { get; set; }
        public decimal Value { get; set; }
        public string ValueString => Value.ToString("N2");
        public int Id { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
        public bool IsField { get; set; }
        public int Sequence { get; set; }
    }

    public class ReportedList
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string CompanyContact { get; set; }
        public string CompanyAddress { get; set; }
        public string FullName => $"{LastName}, {FirstName}";
        public int Month { get; set; }
        public int Year { get; set; }
        public string Date => new DateTime(Year, Month, 1).ToString("MMM - yyyy");
        public bool IsLock { get; set; }
        public string Status => IsLock ? "Locked" : "Unlock";
        public bool IsAdmin { get; set; }
        public IList<ReportedViewList> List { get; set; }
    }
}
