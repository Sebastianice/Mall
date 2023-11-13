using System.ComponentModel.DataAnnotations;

namespace Mall.Services.Models
{

    public class IndexConfigUpdateParams
    {
        [Required(ErrorMessage ="configid不为空")]
        public long ConfigId { get; set; }
        [MaxLength(20)]
        public string? ConfigName { get; set; }
        public string? RedirectUrl { get; set; }
        public sbyte ConfigType { get; set; }
        public long GoodsId { get; set; }
        [Range(0,200)]
        public int ConfigRank { get; set; }
    }


}
