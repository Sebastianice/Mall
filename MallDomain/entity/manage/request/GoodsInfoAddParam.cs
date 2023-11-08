namespace MallDomain.entity.mannage.request
{
    public class GoodsInfoAddParam
    {
        public string? GoodsName { get; set; }
        public string? GoodsIntro { get; set; }
        public int GoodsCategoryId { get; set; }
        public string GoodsCoverImg { get; set; }
        public string? GoodsCarousel { get; set; }
        public string? GoodsDetailContent { get; set; }
        public string? OriginalPrice { get; set; }
        public string? SellingPrice { get; set; }
        public string? StockNum { get; set; }
        public string Tag { get; set; }
        public string GoodsSellStatus { get; set; }
    }

}
