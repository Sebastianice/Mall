namespace Mall.Repository.Models
{
    public class GoodsInfo
    {
        /// <summary>
        /// 商品表主键id
        /// </summary>
        public long GoodsId { get; set; }

        /// <summary>
        /// 商品名
        /// </summary>
        public string GoodsName { get; set; } = null!;

        /// <summary>
        /// 商品简介
        /// </summary>
        public string GoodsIntro { get; set; } = null!;

        /// <summary>
        /// 关联分类id
        /// </summary>
        public long GoodsCategoryId { get; set; }

        /// <summary>
        /// 商品主图
        /// </summary>
        public string GoodsCoverImg { get; set; } = null!;

        /// <summary>
        /// 商品轮播图
        /// </summary>
        public string GoodsCarousel { get; set; } = null!;

        /// <summary>
        /// 商品详情
        /// </summary>
        public string GoodsDetailContent { get; set; } = null!;

        /// <summary>
        /// 商品价格
        /// </summary>
        public int OriginalPrice { get; set; }

        /// <summary>
        /// 商品实际售价
        /// </summary>
        public int SellingPrice { get; set; }

        /// <summary>
        /// 商品库存数量
        /// </summary>
        public int StockNum { get; set; }

        /// <summary>
        /// 商品标签
        /// </summary>
        public string Tag { get; set; } = null!;

        /// <summary>
        /// 商品上架状态 1-下架 0-上架
        /// </summary>
        public sbyte GoodsSellStatus { get; set; }

        /// <summary>
        /// 添加者主键id
        /// </summary>
        public int CreateUser { get; set; }

        /// <summary>
        /// 商品添加时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改者主键id
        /// </summary>
        public int UpdateUser { get; set; }

        /// <summary>
        /// 商品修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
