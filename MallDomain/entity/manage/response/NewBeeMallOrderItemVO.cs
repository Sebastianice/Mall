namespace MallDomain.entity.mannage.response
{
    public class NewBeeMallOrderItemVO
    {
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
