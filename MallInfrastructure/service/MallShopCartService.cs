using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using MallDomain.entity.mall;
using MallDomain.entity.mall.request;
using MallDomain.entity.mall.response;
using MallDomain.entity.mannage;
using MallDomain.service.mall;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace MallInfrastructure.service {
    public class MallShopCartService : IMallShopCartService {

        private readonly MallContext mallContext;
        private MallGoodsInfo _;

        public MallShopCartService(MallContext mallContext) {
            this.mallContext = mallContext;
        }

        public Task DeleteMallCartItem(string token, int id) {
            throw new NotImplementedException();
        }


        public async Task<List<CartItemResponse>> GetCartItemsForSettle(string token, List<long> cartItemIds) {
            var v = await mallContext.MallUserTokens.Where(t => t.Token == token).SingleOrDefaultAsync();
            if (v is null) {
                throw new Exception("不存在用户");
            }
            MallShoppingCartItem[]? shopCartItems = await mallContext.MallShoppingCartItems.
                Where(w => w.UserId == v.UserId && cartItemIds.Contains(w.CartItemId)).AsNoTracking().ToArrayAsync();
            List<CartItemResponse>? cartItemsRes = await getMallShoppingCartItemVOS(shopCartItems);
            return cartItemsRes;
        }


        //购物车转换
        private async Task<List<CartItemResponse>> getMallShoppingCartItemVOS(MallShoppingCartItem[] cartItems) {
            List<long> ids = new List<long>();
            foreach (var item in cartItems) {
                ids.Add(item.GoodsId);
            }
            var newBeeMallGoods = await mallContext.MallGoodsInfos.Where(g => ids.Contains(g.GoodsId)).AsNoTracking().ToArrayAsync();

            var newBeeMallGoodsMap = new Dictionary<long, MallGoodsInfo>();
            foreach (var item in newBeeMallGoods)
            {
                newBeeMallGoodsMap[item.GoodsId] = item;
            }
            List<CartItemResponse> cartItemsRes = new();
            foreach (MallShoppingCartItem item in cartItems) {
                CartItemResponse? cartItemRes = item.Adapt<CartItemResponse>();
                if (newBeeMallGoodsMap.TryGetValue(cartItemRes.GoodsId,out MallGoodsInfo? v)){
                    
                    cartItemRes.GoodsCoverImg = v.GoodsCoverImg;
                    cartItemRes.GoodsName = v.GoodsName;
                    cartItemRes.SellingPrice = v.SellingPrice;
                    cartItemsRes.Add(cartItemRes);
                }
            }
           return cartItemsRes;
        }

        public Task<List<CartItemResponse>> GetMyShoppingCartItems(string token) {
            throw new NotImplementedException();
        }

        public Task SaveMallCartItem(string token, SaveCartItemParam req) {
            throw new NotImplementedException();
        }

        public Task UpdateMallCartItem(string token, UpdateCartItemParam req) {
            throw new NotImplementedException();
        }
    }
}
