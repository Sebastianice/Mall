using MallDomain.entity.common.request;

namespace MallDomain.entity.mannage.request
{
    public class MallGoodsCategoryReq
    {
        public int CategoryId { get; set; }
        public int CategoryLevel { get; set; }
        public int ParentId { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryRank { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }

    public class SearchCategoryParams
    {
        public PageInfo? PageInfo { get; set; }
        public int CategoryLevel { get; set; }
        public int ParentId { get; set; }
    }

}
