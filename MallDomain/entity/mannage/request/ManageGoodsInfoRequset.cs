using MallDomain.entity.common.request;

namespace MallDomain.entity.mannage.request {
    public class MallGoodsInfoSearch {
        public PageInfo PageInfo { get; set; }
        public MallGoodsInfo MallGoodsInfo { get; set; }
    }

    public class GoodsInfoAddParam {
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

    public class GoodsInfoUpdateParam {
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

    public class StockNumDTO {
        public int GoodsId { get; set; }
        public int GoodsCount { get; set; }
    }

}
