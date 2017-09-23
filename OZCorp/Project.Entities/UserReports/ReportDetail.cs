using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Entities.UserReports
{
    [Table("ReportDetail")]
    public class ReportDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
        public bool IsField { get; set; }
        public int Sequence { get; set; }
        public virtual ICollection<UserReportDetail> UserReportDetail { get; set; }
    }
}
