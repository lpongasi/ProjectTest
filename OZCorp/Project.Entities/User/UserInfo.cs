using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Project.Entities.JobOrder;
using Project.Entities.Product;
using Project.Entities.PurchaseOrder;
using Project.Entities.UserReports;

namespace Project.Entities.User
{
    [Table("UserInfo")]
    public class UserInfo
    {
        [Key]
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string CompanyName { get; set; }
        public string CompanyContact { get; set; }
        public string CompanyAddress { get; set; }
        public string FullName => $"{LastName}, {FirstName}";
        public virtual ICollection<UserInfoHistory> UserInfoHistories { get; set; }
        public virtual ICollection<UserInfoHistory> ActionUserInfoHistories { get; set; }
        public virtual ICollection<ItemHistory> ItemHistories { get; set; }
        public virtual ICollection<JobOrderHistory> JobOrderHistories { get; set; }
        public virtual ICollection<PurchaseOrderHistory> PurchaseOrderHistories { get; set; }
        public virtual ICollection<UserReport> UserReport { get; set; }
    }
}
