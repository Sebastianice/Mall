using System.ComponentModel.DataAnnotations;

namespace Mall.Services.Models
{

    public class CarouselUpdateParam
    {
        [Required(ErrorMessage = "id不为空")]
        public int CarouselId { get; set; }

        public string? CarouselUrl { get; set; }

        public string? RedirectUrl { get; set; }

        public int? CarouselRank { get; set; }
    }

}
