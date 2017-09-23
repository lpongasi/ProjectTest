using Project.Common.Enums;

namespace Project.Models.JobOrder
{
    public class JOActionViewModel
    {
        public JoStatus Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ClassIcon { get; set; }
        public string ClassName { get; set; }
    }
}
