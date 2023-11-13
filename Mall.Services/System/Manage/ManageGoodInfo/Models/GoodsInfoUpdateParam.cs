using System.ComponentModel.DataAnnotations;

namespace Mall.Services.Models
{
    public class GoodsInfoUpdateParam
    {
        [MaxLength(128)]
        public string GoodsName { get; set; } = null!;

        [Required(ErrorMessage ="goodId不为空")]
        public long GoodsId { get; set; }
        [MaxLength(200)]
        public string? GoodsIntro { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "id值必须大于1")]
        public int GoodsCategoryId { get; set; }
        [Required(ErrorMessage = "图片不为空")]
        public string GoodsCoverImg { get; set; } = "";


        public string? GoodsCarousel { get; set; }
        [Required(ErrorMessage = "商品内容不为空")]
        public string? GoodsDetailContent { get; set; }
        [Range(1, 1000000, ErrorMessage = "原价格不能超出1000000")]

        public int OriginalPrice { get; set; }
        [Range(1, 1000000, ErrorMessage = "价格不能超出1000000")]
        public int SellingPrice { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "库存不能小于1")]
        public int StockNum { get; set; }
        [MaxLength(16, ErrorMessage = "tag长度不能超过16")]
        public string Tag { get; set; } = "";
        public sbyte GoodsSellStatus { get; set; } = 0;


        public int UpdateUser { get; set; }


        public DateTime UpdateTime { get; set; }
    }

}
