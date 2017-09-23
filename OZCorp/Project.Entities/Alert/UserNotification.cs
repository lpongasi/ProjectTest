using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Entities.Alert
{
    [Table("UserNotification")]
    public class UserNotification
    {
        [Key,Column(Order = 1)]
        public string UserId { get; set; }
        [Key,Column(Order = 2)]
        public long NotificationId { get; set; }
        public bool IsViewed { get; set; }
        [ForeignKey("NotificationId")]
        public virtual Notification Notification { get; set; }
    }
}
