namespace MallDomain.entity.mannage
{
    public class GoodsCategory
    {
        /// <summary>
        /// 分类id
        /// </summary>
        public long CategoryId { get; set; }

        /// <summary>
        /// 分类级别(1-一级分类 2-二级分类 3-三级分类)
        /// </summary>
        public sbyte CategoryLevel { get; set; }

        /// <summary>
        /// 父分类id
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        public string CategoryName { get; set; } = null!;

        /// <summary>
        /// 排序值(字段越大越靠前)
        /// </summary>
        public int CategoryRank { get; set; }

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
        public int? UpdateUser { get; set; }
    }
}
