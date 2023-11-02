namespace MallDomain.entity.mannage {
    public class MallCarousel {

        public long CarouselId { get; set; }
        public string? CarouselUrl { get; set; }
        public string? RedirectUrl { get; set; }
        public int CarouselRank { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateTime { get; set; }
        public long CreateUser { get; set; }
        public DateTime UpdateTime { get; set; }
        public long UpdateUser { get; set; }
    }
}
