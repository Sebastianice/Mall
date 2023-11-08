namespace MallDomain.entity.mall.response
{
    public class MallOrderDetailVO
    {
        public string? OrderNo { get; set; }

        public int TotalPrice { get; set; }

        public int PayType { get; set; }
        public string? PayTypeString { get; set; }
        public DateTime PayTime { get; set; }
        public int OrderStatus { get; set; }
        public string? OrderStatusString { get; set; }
        public DateTime CreateTime { get; set; }

        public List<NewBeeMallOrderItemVO>? NewBeeMallOrderItemVOS { get; set; }
    }
}
