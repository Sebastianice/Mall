﻿using MallDomain.entity.mall;
using MallDomain.entity.mall.request;
using MallDomain.entity.mall.response;

namespace MallDomain.service.mall {
    public interface IMallShopCartService {
        // GetMyShoppingCartItems 不分页
        public Task<List<CartItemResponse>> GetMyShoppingCartItems(string token);
        public Task SaveMallCartItem(string token, SaveCartItemParam req);
        public Task UpdateMallCartItem(string token, UpdateCartItemParam req);

        public Task DeleteMallCartItem(string token, int id);
        public Task<List<CartItemResponse>> GetCartItemsForSettle(string token, int[] cartItemIds);

        // 购物车数据转换
        Task<List<CartItemResponse>> getMallShoppingCartItemVOS(MallShoppingCartItem[] cartItems);
    }
}
