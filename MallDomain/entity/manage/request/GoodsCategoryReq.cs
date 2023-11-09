namespace MallDomain.entity.mannage.request
{
    public class GoodsCategoryReq
    {
        public int CategoryId { get; set; }
        public int CategoryLevel { get; set; }
        public int ParentId { get; set; }
        public string? CategoryName { get; set; }
        public int CategoryRank { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }

}
