namespace MallDomain.entity.mall
{
    public class MallUserAddress
    {

        public long AddressId { get; set; }
        public long UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserPhone { get; set; }
        public bool DefaultFlag { get; set; }
        public string? ProvinceName { get; set; }

        public string? CityName { get; set; }
        public string? RegionName { get; set; }
        public string? DetailAddress { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
