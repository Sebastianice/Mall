using System.ComponentModel.DataAnnotations;

namespace Mall.Services.Models
{
    public class CarouselAddParam
    {
        [Required(ErrorMessage = "CarouselUrl不为空")]
        public string? CarouselUrl { get; set; }
        [Required(ErrorMessage = "CarouselUrl不为空")]
        public string? RedirectUrl { get; set; }
        [Range(0,200,ErrorMessage = "CarouselUrl不为空")]
        public int? CarouselRank { get; set; }
    }

}
