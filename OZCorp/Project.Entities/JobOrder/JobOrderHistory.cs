using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Project.Entities.User;

namespace Project.Entities.JobOrder
{
    [Table("JobOrderHistory")]
    public class JobOrderHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string ActionUserId { get; set; }
        public string Action { get; set; }
        public DateTime ActionDate { get; set; }
        public long JobOrderId { get; set; }

        [ForeignKey("JobOrderId")]
        public virtual JobOrder JobOrder { get; set; }
        [ForeignKey("ActionUserId")]
        public virtual UserInfo ActionUser { get; set; }
    }
}
