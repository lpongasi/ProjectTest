using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Entities.Block
{
    public class ItemBlock
    {
        [Key, Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Key, Column(Order = 1)]
        public string GroupId { get; set; }
        public string Type { get; set; }
        public string LogoUrl { get; set; }
        public string BackgroundUrl { get; set; }
        public string ItemUrl { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
