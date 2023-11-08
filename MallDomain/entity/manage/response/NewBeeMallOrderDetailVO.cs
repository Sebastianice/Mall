namespace MallDomain.entity.mannage.response
{
    public class NewBeeMallOrderDetailVO
    {
        public int OrderId { get; set; }
        public string? OrderNo { get; set; }
        public int TotalPrice { get; set; }
        public int PayType { get; set; }
        public string? PayTypeString { get; set; }
        public int OrderStatus { get; set; }
        public string? OrderStatusString { get; set; }
        public DateTime CreateTime { get; set; }
        public List<NewBeeMallOrderItemVO> NewBeeMallOrderItemVOS { get; set; } = new List<NewBeeMallOrderItemVO>();
    }

}
