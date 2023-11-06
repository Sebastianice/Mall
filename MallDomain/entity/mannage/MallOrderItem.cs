namespace MallDomain.entity.mannage
{
    public class MallOrderItem
    {
        public long OrderItemId { get; set; }
        public long OrderId { get; set; }
        public long GoodsId { get; set; }
        public string? GoodsName { get; set; }
        public string? GoodsCoverImg { get; set; }
        public int SellingPrice { get; set; }
        public int GoodsCount { get; set; }
        public DateTime CreateTime { get; set; }

    }
}
