namespace MallDomain.entity.mall
{
    public class UserAddress
    {

        public long AddressId { get; set; }

        /// <summary>
        /// 用户主键id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string UserName { get; set; } = null!;

        /// <summary>
        /// 收货人手机号
        /// </summary>
        public string UserPhone { get; set; } = null!;

        /// <summary>
        /// 是否为默认 0-非默认 1-是默认
        /// </summary>
        public sbyte DefaultFlag { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        public string ProvinceName { get; set; } = null!;

        /// <summary>
        /// 城
        /// </summary>
        public string CityName { get; set; } = null!;

        /// <summary>
        /// 区
        /// </summary>
        public string RegionName { get; set; } = null!;

        /// <summary>
        /// 收件详细地址(街道/楼宇/单元)
        /// </summary>
        public string DetailAddress { get; set; } = null!;

        /// <summary>
        /// 删除标识字段(0-未删除 1-已删除)
        /// </summary>
        public sbyte IsDeleted { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
