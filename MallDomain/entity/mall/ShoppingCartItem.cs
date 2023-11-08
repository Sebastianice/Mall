namespace MallDomain.entity.mall
{
    public class ShoppingCartItem
    {

        /// <summary>
        /// 购物项主键id
        /// </summary>
        public long CartItemId { get; set; }

        /// <summary>
        /// 用户主键id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 关联商品id
        /// </summary>
        public long GoodsId { get; set; }

        /// <summary>
        /// 数量(最大为5)
        /// </summary>
        public int GoodsCount { get; set; }

        /// <summary>
        /// 删除标识字段(0-未删除 1-已删除)
        /// </summary>
        public sbyte IsDeleted { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 最新修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

    }
}
