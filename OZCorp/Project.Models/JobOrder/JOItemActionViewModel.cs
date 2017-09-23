using Project.Common.Enums;

namespace Project.Models.JobOrder
{
    public class JOItemActionViewModel
    {
        public JoItemStatus Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ClassIcon { get; set; }
        public string ClassName { get; set; }
        public JoItemStatus CurrentStatus { get; set; }
    }
}
