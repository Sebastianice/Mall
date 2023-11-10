using MallDomain.entity.common.request;

namespace MallDomain.entity.mannage.request
{
    public class SearchCategoryParams
    {
        public PageInfo PageInfo { get; set; }
        public int CategoryLevel { get; set; }
        public int ParentId { get; set; }
    }

}
