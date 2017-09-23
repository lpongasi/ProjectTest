using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Project.Entities.Alert
{
    [Table("Notification")]
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string CommonId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public DateTime NotifDate { get; set; }
        public string NotifDateString => NotifDate.ToString("MMM dd, yyyy h:mm:ss tt");
        public virtual ICollection<UserNotification> UserNotifications { get; set; }
    }
}
