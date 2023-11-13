namespace Mall.Services.Models
{
    public class GoodsInfoDetailResponse
    {
        public long GoodsId { get; set; }
        public string? GoodsName { get; set; }
        public string? GoodsIntro { get; set; }
        public string? GoodsCoverImg { get; set; }
        public int SellingPrice { get; set; }
        public string? GoodsDetailContent { get; set; }
        public int OriginalPrice { get; set; }
        public string? Tag { get; set; }
        public List<string>? GoodsCarouselList { get; set; }
    }
}
