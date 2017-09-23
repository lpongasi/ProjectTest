using System.Collections.Generic;

namespace Project.Common.Common
{
    public class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
    public class Response<T> : Response
    {
        public T Data { get; set; }
    }
    public class ResponseList<T> : Response<IEnumerable<T>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public int PageCount => PageSize != 0 && Total != 0 ? (Total / PageSize) + ((Total%PageSize)!=0?1:0) : 0;
    }
    public class ResponseFilter<T> : Response<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public int PageCount => PageSize != 0 && Total != 0 ? (Total / PageSize) + ((Total % PageSize) != 0 ? 1 : 0) : 0;
    }
}
