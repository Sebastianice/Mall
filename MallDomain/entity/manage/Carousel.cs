namespace MallDomain.entity.mannage
{
    public class Carousel
    {

        /// <summary>
        /// 首页轮播图主键id
        /// </summary>
        public int CarouselId { get; set; }

        /// <summary>
        /// 轮播图
        /// </summary>
        public string CarouselUrl { get; set; } = null!;

        /// <summary>
        /// 点击后的跳转地址(默认不跳转)
        /// </summary>
        public string RedirectUrl { get; set; } = null!;

        /// <summary>
        /// 排序值(字段越大越靠前)
        /// </summary>
        public int CarouselRank { get; set; }

        /// <summary>
        /// 删除标识字段(0-未删除 1-已删除)
        /// </summary>
        public sbyte IsDeleted { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建者id
        /// </summary>
        public int CreateUser { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 修改者id
        /// </summary>
        public int UpdateUser { get; set; }
    }
}
