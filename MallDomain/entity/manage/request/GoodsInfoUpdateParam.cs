namespace MallDomain.entity.mannage.request
{
    public class GoodsInfoUpdateParam
    {
        public string GoodsId { get; set; }
        public string GoodsName { get; set; }
        public string GoodsIntro { get; set; }
        public int GoodsCategoryId { get; set; }
        public string GoodsCoverImg { get; set; }
        public string GoodsCarousel { get; set; }
        public string GoodsDetailContent { get; set; }
        public string OriginalPrice { get; set; }
        public int SellingPrice { get; set; }
        public string StockNum { get; set; }
        public string Tag { get; set; }
        public int GoodsSellStatus { get; set; }


        public int UpdateUser { get; set; }


        public DateTime UpdateTime { get; set; }
    }

}
