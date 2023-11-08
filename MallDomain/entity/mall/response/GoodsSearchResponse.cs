namespace MallDomain.entity.mall.response
{
    public class GoodsSearchResponse
    {
        public long GoodsId { get; set; }
        public string? GoodsName { get; set; }
        public string? GoodsInfo { get; set; }
        public string? GoodsCoverImg { get; set; }
        public int SellingPrice { get; set; }
    }
}
