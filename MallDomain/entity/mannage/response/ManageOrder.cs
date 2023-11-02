namespace MallDomain.entity.mannage.response {
    public class NewBeeMallOrderDetailVO {
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

    public class NewBeeMallOrderItemVO {
        public int OrderItemId { get; set; }
        public int GoodsId { get; set; }
        public string? GoodsName { get; set; }
        public string? GoodsCoverImg { get; set; }
        public int SellingPrice { get; set; }
        public int GoodsCount { get; set; }
        public int GoodsSpecificationIds { get; set; }
        public string? GoodsSpecificationNames { get; set; }
    }

}
