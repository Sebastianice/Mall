namespace Mall.Repository.Models
{
    public class IndexConfig
    {

        /// <summary>
        /// 首页配置项主键id
        /// </summary>
        public long ConfigId { get; set; }

        /// <summary>
        /// 显示字符(配置搜索时不可为空，其他可为空)
        /// </summary>
        public string ConfigName { get; set; } = null!;

        /// <summary>
        /// 1-搜索框热搜 2-搜索下拉框热搜 3-(首页)热销商品 4-(首页)新品上线 5-(首页)为你推荐
        /// </summary>
        public sbyte ConfigType { get; set; }

        /// <summary>
        /// 商品id 默认为0
        /// </summary>
        public long GoodsId { get; set; }

        /// <summary>
        /// 点击后的跳转地址(默认不跳转)
        /// </summary>
        public string RedirectUrl { get; set; } = null!;

        /// <summary>
        /// 排序值(字段越大越靠前)
        /// </summary>
        public int ConfigRank { get; set; }

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
        /// 最新修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 修改者id
        /// </summary>
        public int? UpdateUser { get; set; }
    }
}
