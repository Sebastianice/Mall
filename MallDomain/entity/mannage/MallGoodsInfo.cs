namespace MallDomain.entity.mannage
{
    public class MallGoodsInfo
    {
        public long GoodsId { get; set; }
        public string? GoodsName { get; set; }
        public string? GoodsIntro { get; set; }
        public long GoodsCategoryId { get; set; }
        public string? GoodsCoverImg { get; set; }
        public string? GoodsCarousel { get; set; }
        public string? GoodsDetailContent { get; set; }
        public int OriginalPrice { get; set; }

        public int SellingPrice { get; set; }
        public int StockNum { get; set; }
        public string? Tag { get; set; }
        public int GoodsSellStatus { get; set; }
        public long CreateUser { get; set; }
        public DateTime CreateTime { get; set; }
        public long UpdateUser { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
