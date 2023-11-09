namespace MallDomain.entity.mannage.request
{

    public class CarouselUpdateParam
    {
        public int CarouselId { get; set; }

        public string? CarouselUrl { get; set; }

        public string? RedirectUrl { get; set; }

        public int? CarouselRank { get; set; }
    }

}
