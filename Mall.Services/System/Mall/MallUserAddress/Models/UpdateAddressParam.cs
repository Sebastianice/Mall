namespace Mall.Services.Models
{
    public class UpdateAddressParam
    {

        public long? AddressId { get; set; }
        public long? UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserPhone { get; set; }
        public bool DefaultFlag { get; set; }
        public string? ProvinceName { get; set; }
        public string? CityName { get; set; }
        public string? RegionName { get; set; }
        public string? DetailAddress { get; set; }
    }
}
