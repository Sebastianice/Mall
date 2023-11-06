

namespace MallDomain.entity.mall.request
{
    public class GoodsSearchParams
    {
        public string? Keyword { get; set; }

        public int GoodsCategoryId { get; set; }

        public string? OrderBy { get; set; }

        public int PageNumber { get; set; }
    }


}
