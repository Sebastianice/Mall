namespace MallDomain.entity.mall.request {
   public class AddAddressParam {

        public string? UserName{ get; set; }
        public string? UserPhone{ get; set; }
        public byte? DefaultFlag{ get; set; }
        public string? ProvinceName{ get; set; }
        public string? CityName{ get; set; }
        public string? RegionName{ get; set; }
        public string? DetailAddress{ get; set; }
    }
   public class UpdateAddressParam {

        public string? AddressId{ get; set; }
        public long? UserId{ get; set; }
        public string? UserName{ get; set; }
        public string? UserPhone{ get; set; }
        public byte? DefaultFlag{ get; set; }
        public string? ProvinceName{ get; set; }
        public string? CityName{ get; set; }
        public string? RegionName{ get; set; }
        public string? DetailAddress{ get; set; }
    }
}
