using Project.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Entities.JobOrder
{
    [Table("JobOrderItemAction")]
    public class JobOrderItemAction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public JoItemStatus StatusId { get; set; }
        public JoItemStatus ValidStatusId { get; set; }
        public JoItemStatus NextStatusId { get; set; }

        [ForeignKey("StatusId")]
        public virtual JobOrderItemStatus Status { get; set; }
        [ForeignKey("ValidStatusId")]
        public virtual JobOrderItemStatus ValidStatus { get; set; }
        [ForeignKey("NextStatusId")]
        public virtual JobOrderItemStatus NextStatus { get; set; }
    }
}
