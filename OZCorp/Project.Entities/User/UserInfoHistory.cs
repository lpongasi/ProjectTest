using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Entities.User
{
    [Table("UserInfoHistory")]
    public class UserInfoHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string UserId { get; set; }
        public string ActionUserId { get; set; }
        public string Action { get; set; }
        public DateTime ActionDate { get; set; }
        [ForeignKey("UserId")]
        public virtual UserInfo User { get; set; }
        [ForeignKey("ActionUserId")]
        public virtual UserInfo ActionUser { get; set; }
    }
}
