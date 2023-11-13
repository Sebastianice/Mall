namespace Mall.Repository.Models
{
    public class Order
    {  /// <summary>
       /// 订单表主键id
       /// </summary>
        public long OrderId { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; } = null!;

        /// <summary>
        /// 用户主键id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 订单总价
        /// </summary>
        public int TotalPrice { get; set; }

        /// <summary>
        /// 支付状态:0.未支付,1.支付成功,-1:支付失败
        /// </summary>
        public sbyte PayStatus { get; set; }

        /// <summary>
        /// 0.无 1.支付宝支付 2.微信支付
        /// </summary>
        public sbyte PayType { get; set; }

        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime? PayTime { get; set; }

        /// <summary>
        /// 订单状态:0.待支付 1.已支付 2.配货完成 3:出库成功 4.交易成功 -1.手动关闭 -2.超时关闭 -3.商家关闭
        /// </summary>
        public sbyte OrderStatus { get; set; }

        /// <summary>
        /// 订单body
        /// </summary>
        public string ExtraInfo { get; set; } = null!;

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
