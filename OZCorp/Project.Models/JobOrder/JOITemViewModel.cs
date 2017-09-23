using System;
using Project.Common.Enums;

namespace Project.Models.JobOrder
{
    public class JOITemViewModel
    {
        public long JobOrderId { get; set; }
        public long ItemId { get; set; }
        public string Barcode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public JoItemStatus StatusId { get; set; }
        public int Qty { get; set; }
        public string Size { get; set; }

        public DateTime? DateTimeStart { get; set; }
        public DateTime? DateTimeEnd { get; set; }
        public int EstDay { get; set; }
        public int EstHour { get; set; }
        public int EstMinute { get; set; }
        public bool CanEstimate => StatusId == JoItemStatus.Pending;
        public TimeSpan EstimatedTime => new TimeSpan(EstDay,EstHour,EstMinute,0);

        public string StartDate => DateTimeStart?.ToString("g") ?? "N/A";
        public string EndDate => DateTimeEnd?.ToString("g") ?? "N/A";
        public string TimeConsumed => DateTimeStart.HasValue && DateTimeEnd.HasValue
                                     ? (DateTimeEnd.Value - DateTimeStart.Value).ToString("g")
            : "N/A";
        public string EstDiff => DateTimeStart.HasValue && DateTimeEnd.HasValue
            ? EstimatedTime.Subtract(DateTimeEnd.Value - DateTimeStart.Value).ToString("g")
            : "N/A";

        public JOItemActionViewModel Action { get; set; }
    }
}
