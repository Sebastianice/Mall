using System.ComponentModel.DataAnnotations;

namespace Mall.Services.Models
{
    public class IndexConfigAddParams
    {
        [Required(ErrorMessage ="configName不为空")]
        public string? ConfigName { get; set; }
        public sbyte ConfigType { get; set; }
        [Required(ErrorMessage ="id不为空")]
        public long GoodsId { get; set; }
        public string? RedirectUrl { get; set; }
        [Range(0,200,ErrorMessage ="range不能超0-200")]
        public int ConfigRank { get; set; }
    }


}
