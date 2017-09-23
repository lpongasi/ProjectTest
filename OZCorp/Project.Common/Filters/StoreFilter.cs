namespace Project.Common.Filters
{
    public class StoreFilter : CommonFilter<string>
    {
        public int Category { get; set; }
        public int SubCategory { get; set; }
    }
}
