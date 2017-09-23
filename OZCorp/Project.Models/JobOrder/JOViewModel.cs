using System;
using System.Collections.Generic;
using Project.Common.Enums;

namespace Project.Models.JobOrder
{
    public class JOViewModel
    {
        public JOViewModel()
        {
            Items = new List<JOITemViewModel>();
            Actions = new List<JOActionViewModel>();
        }
        public long Id { get; set; }
        public long? PoId { get; set; }
        public string IdString => $"JO{Id}";
        public string PoIdString => PoId.HasValue? $"PO{PoId}" : string.Empty;
        public DateTime JoDate { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string CompanyContact { get; set; }
        public string CompanyAddress { get; set; }
        public string FullName => $"{LastName}, {FirstName}";
        public string JoStatus { get; set; }
        public JoStatus JoStatusId { get; set; }
        public IEnumerable<JOITemViewModel> Items { get; set; }
        public IEnumerable<JOActionViewModel> Actions { get; set; }        
        public string JoDateString => JoDate.ToString("MMM dd, yyyy");
        public bool Printable { get; set; }
        public bool Editable { get; set; }
        

        public string MainCompanyName { get; set; }
        public string MainCompanyContact { get; set; }
        public string MainCompanyAddress { get; set; }
    }
}
