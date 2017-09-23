using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Entities.Global
{
    [Table("GlobalImage")]
    public class GlobalImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string FileName { get; set; }
        public string FileLocation { get; set; }
        public bool IsActive { get; set; }
        public int Sequence { get; set; }

    }
}
