namespace Mall.Services.Models
{
    public class NewBeeMallOrderItemVO
    {
        public long GoodsId { get; set; }
        public int GoodsCount { get; set; }
        public string? GoodsName { get; set; }


        public string? GoodsCoverImg { get; set; }
        public int SellingPrice { get; set; }

    }
}
