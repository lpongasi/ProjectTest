namespace Project.Common.Filters
{
    public class CommonFilter
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
    public class CommonFilter<T> : CommonFilter
    {
        public T Filter { get; set; }
    }
}
