using System.ComponentModel.DataAnnotations;

namespace Mall.Services.Models
{
    public class GoodsCategoryReq
    {
        public int CategoryId { get; set; }
        public int CategoryLevel { get; set; }
        public int ParentId { get; set; }

        [Required(ErrorMessage = "CategoryName不能为空")]
        public string? CategoryName { get; set; }

        [Required(ErrorMessage = "CategoryRank不能为空")]
        public int CategoryRank { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }

}
