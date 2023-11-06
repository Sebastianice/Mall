using MallDomain.entity.common.request;

namespace MallDomain.entity.mannage.request
{
    public class MallCarouselSearch
    {
        public MallCarousel? MallCarousel { get; set; }
        public PageInfo? PageInfo { get; set; }
    }

    public class MallCarouselAddParam
    {
        public string? CarouselUrl { get; set; }

        public string? RedirectUrl { get; set; }

        public string? CarouselRank { get; set; }
    }

    public class MallCarouselUpdateParam
    {
        public int CarouselId { get; set; }

        public string? CarouselUrl { get; set; }

        public string? RedirectUrl { get; set; }

        public string? CarouselRank { get; set; }
    }

}
