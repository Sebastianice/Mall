using MallDomain.entity.mall.request;
using MallDomain.entity.mall.response;

namespace MallDomain.service.mall
{
    public interface IMallShopCartService
    {
        // GetMyShoppingCartItems 不分页
        public Task<List<CartItemResponse>> GetMyShoppingCartItems(string token);
        public Task SaveMallCartItem(string token, SaveCartItemParam req);
        public Task UpdateMallCartItem(string token, UpdateCartItemParam req);

        public Task DeleteMallCartItem(string token, long id);
        public Task<List<CartItemResponse>> GetCartItemsForSettle(string token, List<long> cartItemIds);


    }
}
