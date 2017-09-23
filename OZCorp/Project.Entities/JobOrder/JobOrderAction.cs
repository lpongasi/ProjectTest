using Project.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Entities.JobOrder
{
    [Table("JobOrderAction")]
    public class JobOrderAction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public JoStatus StatusId { get; set; }
        public JoStatus ValidStatusId { get; set; }
        public JoStatus NextStatusId { get; set; }

        [ForeignKey("StatusId")]
        public virtual JobOrderStatus Status { get; set; }
        [ForeignKey("ValidStatusId")]
        public virtual JobOrderStatus ValidStatus { get; set; }
        [ForeignKey("NextStatusId")]
        public virtual JobOrderStatus NextStatus { get; set; }
    }
}
