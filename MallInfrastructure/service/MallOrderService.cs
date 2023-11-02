using MallDomain.entity.mall;
using MallDomain.entity.mall.response;
using MallDomain.service.mall;

namespace MallInfrastructure.service {
    public class MallOrderService : IMallOrderService {
        public Task CancelOrder(string token, string orderNo) {
            throw new NotImplementedException();
        }

        public Task FinishOrder(string token, string orderNo) {
            throw new NotImplementedException();
        }

        public Task<MallOrderDetailVO> GetOrderDetailByOrderNo(string token, string orderNo) {
            throw new NotImplementedException();
        }

        public Task<(MallOrderResponse[] list, long total)> MallOrderListBySearch(string token, int pageNumber, string status) {
            throw new NotImplementedException();
        }

        public Task PaySuccess(string orderNo, int payType) {
            throw new NotImplementedException();
        }

        public Task<string> SaveOrder(string token, MallUserAddress userAddress, CartItemResponse[] myShoppingCartItems) {
            throw new NotImplementedException();
        }
    }
}
