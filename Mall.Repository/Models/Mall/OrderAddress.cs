namespace Mall.Repository.Models
{
    public class OrderAddress
    {
        public long OrderId { get; set; }

        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string UserName { get; set; } = null!;

        /// <summary>
        /// 收货人手机号
        /// </summary>
        public string UserPhone { get; set; } = null!;

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

    }
}
