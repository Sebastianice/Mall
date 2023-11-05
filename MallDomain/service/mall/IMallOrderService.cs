using MallDomain.entity.mall;
using MallDomain.entity.mall.response;

namespace MallDomain.service.mall {
    public interface IMallOrderService {
        // SaveOrder 保存订单
        public Task<string> SaveOrder(string token, MallUserAddress userAddress, List<CartItemResponse> myShoppingCartItems);

        // PaySuccess 支付订单
        public Task PaySuccess(string orderNo, int payType);

        // FinishOrder 完结订单
        public Task FinishOrder(string token, string orderNo);

        // CancelOrder 关闭订单
        public Task CancelOrder(string token, string orderNo);

        // GetOrderDetailByOrderNo 获取订单详情
        public Task<MallOrderDetailVO> GetOrderDetailByOrderNo(string token, string orderNo);

        // MallOrderListBySearch 搜索订单
        public Task<(List<MallOrderResponse> list, long total)> MallOrderListBySearch(string token, int pageNumber, string status);
    }
}
