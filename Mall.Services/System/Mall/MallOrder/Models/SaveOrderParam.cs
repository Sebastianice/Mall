namespace Mall.Services.Models
{
    public class SaveOrderParam
    {
        public List<long>? CartItemIds { get; set; }

        public int AddressId { get; set; }
    }
}
