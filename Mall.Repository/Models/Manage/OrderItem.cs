namespace Mall.Repository.Models
{
    public class OrderItem
    { /// <summary>
      /// 订单关联购物项主键id
      /// </summary>
        public long OrderItemId { get; set; }

        /// <summary>
        /// 订单主键id
        /// </summary>
        public long OrderId { get; set; }

        /// <summary>
        /// 关联商品id
        /// </summary>
        public long GoodsId { get; set; }

        /// <summary>
        /// 下单时商品的名称(订单快照)
        /// </summary>
        public string GoodsName { get; set; } = null!;

        /// <summary>
        /// 下单时商品的主图(订单快照)
        /// </summary>
        public string GoodsCoverImg { get; set; } = null!;

        /// <summary>
        /// 下单时商品的价格(订单快照)
        /// </summary>
        public int SellingPrice { get; set; }

        /// <summary>
        /// 数量(订单快照)
        /// </summary>
        public int GoodsCount { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

    }
}
