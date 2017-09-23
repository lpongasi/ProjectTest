using Project.Entities.User;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Entities.UserReports
{
    [Table("UserReport")]
    public class UserReport
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string UserId { get; set; }

        public bool IsLock { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        [ForeignKey("UserId")]
        public virtual UserInfo User { get; set; }
        public virtual ICollection<UserReportDetail> UserReportDetail { get; set; }
    }
}
