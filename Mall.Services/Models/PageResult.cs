namespace Mall.Services.Models
{
    public class PageResult
    {
        public Object? List { get; set; }
        public long TotalCount { get; set; }
        public int TotalPage { get; set; }
        public int CurrPage { get; set; }
        public int PageSize { get; set; }
    }
}
