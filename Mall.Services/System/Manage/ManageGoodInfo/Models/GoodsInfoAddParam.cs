using System.ComponentModel.DataAnnotations;

namespace Mall.Services.Models
{
    public class GoodsInfoAddParam
    {
        [MaxLength(128)]
        public string GoodsName { get; set; } = null!;

        [MaxLength(200)]
        public string? GoodsIntro { get; set; }
        [Range(1,int.MaxValue , ErrorMessage = "id值必须大于1")]
        public int GoodsCategoryId { get; set; }
        [Required(ErrorMessage ="图片不为空")]
        public string GoodsCoverImg { get; set; } = "";


        public string? GoodsCarousel { get; set; }
        [Required(ErrorMessage ="商品详情不能为空")]
        public string? GoodsDetailContent { get; set; }
        [Range(1,1000000,ErrorMessage = "原价格不能超出1000000")]
     
        public int OriginalPrice { get; set; }
        [Range(1, 1000000, ErrorMessage = "价格不能超出1000000")]
        public int SellingPrice { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "库存不能小于1")]
        public int StockNum { get; set; }
        [MaxLength(16,ErrorMessage ="tag不能超16")]
        public string Tag { get; set; } = "";
        public string GoodsSellStatus { get; set; } = "";
    }

}
