using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Entities.UserReports
{
    [Table("UserReportDetail")]
    public class UserReportDetail
    {
        [Key,Column(Order = 1)]
        public long UrId { get; set; }
        [Key,Column(Order = 2)]
        public int RdId { get; set; }
        public decimal Value { get; set; }

        [ForeignKey("RdId")]
        public virtual ReportDetail ReportDetail { get; set; }
        [ForeignKey("UrId")]
        public virtual UserReport UserReport { get; set; }

    }
}
