namespace MallDomain.entity.mall.request
{

    public class PaySuccessParams
    {
        public string? OrderNo { get; set; }

        public int PayType { get; set; }
    }
    public class OrderSearchParams
    {
        public string? Status { get; set; }

        public int PageNumber { get; set; }
    }
    public class SaveOrderParam
    {
        public List<long>? CartItemIds { get; set; }

        public int AddressId { get; set; }
    }
}
