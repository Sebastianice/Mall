using MallDomain.entity.mall;
using MallDomain.entity.mall.request;
using MallDomain.entity.mall.response;
using MallDomain.entity.mannage;
using MallDomain.service.mall;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace MallInfrastructure.service.mall
{
    public class MallShopCartService : IMallShopCartService
    {

        private readonly MallContext context;


        public MallShopCartService(MallContext mallContext)
        {
            context = mallContext;
        }

        public async Task DeleteMallCartItem(string token, long id)
        {
            UserToken? userToken = await context.UserTokens.
                SingleOrDefaultAsync(t => t.Token == token);
            if (userToken is null) throw new Exception("不存在用户");

            var item = context.ShoppingCartItems.SingleOrDefault(u => u.UserId == userToken.UserId && u.CartItemId == id);
            if (item is null) throw new Exception("没有相应记录");

            item.IsDeleted = 0;

            await context.SaveChangesAsync();
        }


        public async Task<List<CartItemResponse>> GetCartItemsForSettle(string token, List<long> cartItemIds)
        {
            var v = await context.UserTokens.
                SingleOrDefaultAsync(t => t.Token == token);

            if (v is null) throw new Exception("不存在用户");

            ShoppingCartItem[]? shopCartItems = await context.ShoppingCartItems.
                Where(w => w.UserId == v.UserId && cartItemIds.Contains(w.CartItemId)).
                AsNoTracking().
                ToArrayAsync();

            List<CartItemResponse>? cartItemsRes = await getMallShoppingCartItemVOS(shopCartItems);

            return cartItemsRes;
        }


        //购物车转换
        private async Task<List<CartItemResponse>> getMallShoppingCartItemVOS(ShoppingCartItem[] cartItems)
        {
            List<long> ids = new List<long>();
            foreach (var item in cartItems)
            {
                ids.Add(item.GoodsId);
            }

            var newBeeMallGoods = await context.GoodsInfos.
                Where(g => ids.Contains(g.GoodsId)).
                AsNoTracking().
                ToArrayAsync();

            var newBeeMallGoodsMap = new Dictionary<long, GoodsInfo>();
            foreach (var item in newBeeMallGoods)
            {
                newBeeMallGoodsMap[item.GoodsId] = item;
            }


            List<CartItemResponse> cartItemsRes = new();
            foreach (ShoppingCartItem item in cartItems)
            {
                CartItemResponse? cartItemRes = item.Adapt<CartItemResponse>();
                if (newBeeMallGoodsMap.TryGetValue(cartItemRes.GoodsId, out GoodsInfo? v))
                {
                    cartItemRes.GoodsCoverImg = v.GoodsCoverImg;
                    cartItemRes.GoodsName = v.GoodsName;
                    cartItemRes.SellingPrice = v.SellingPrice;
                    cartItemsRes.Add(cartItemRes);
                }
            }

            return cartItemsRes;
        }

        public async Task<List<CartItemResponse>> GetMyShoppingCartItems(string token)
        {
            var userToken = await context.UserTokens.
                SingleAsync(t => t.Token == token);


            var shopcarts = await context.ShoppingCartItems.
                Where(u => u.UserId == userToken.UserId).
                AsNoTracking().
                ToListAsync();
            if (shopcarts.Count == 0) throw new Exception("购物车内空空如也");

            List<long> goodsIds = new List<long>();
            foreach (var item in shopcarts)
            {
                goodsIds.Add(item.GoodsId);
            }


            //通过商品id找到商品
            var goodInfos = context.GoodsInfos.
                Where(w => goodsIds.Contains(w.GoodsId)).
                AsNoTracking();

            //一个商品对应一个商品信息
            Dictionary<long, GoodsInfo> goodsMap = new();
            foreach (var item in goodInfos)
            {
                goodsMap[item.GoodsId] = item;
            }


            List<CartItemResponse> cartItems = new();
            foreach (var item in shopcarts)
            {
                var cartItem = item.Adapt<CartItemResponse>();
                if (goodsMap.TryGetValue(item.GoodsId, out var value))
                {
                    cartItem.GoodsName = value.GoodsName;
                    cartItem.GoodsCoverImg = value.GoodsCoverImg;
                    cartItem.SellingPrice = value.SellingPrice;
                }

                cartItems.Add(cartItem);
            }

            return cartItems;
        }

        public async Task SaveMallCartItem(string token, SaveCartItemParam req)
        {
            if (req.GoodsCount < 1) throw new Exception("商品数量不能小于1");
            if (req.GoodsCount > 5) throw new Exception("超出单个商品最大购买数量！");


            var userToken = await context.UserTokens.
                AsNoTracking().
                SingleAsync(t => t.Token == token);


            //判断是否存在商品
            var shopItem = await context.ShoppingCartItems.Where(w => w.UserId == userToken.UserId && w.GoodsId == req.GoodsId).AsNoTracking().FirstOrDefaultAsync();
            if (shopItem != null) throw new Exception("商品已存在！无需重复添加");

            var goodInfo = context.GoodsInfos.
                FirstOrDefault(w => w.GoodsId == req.GoodsId);

            if (goodInfo == null) throw new Exception("商品为空");

            var total = await context.ShoppingCartItems.
                CountAsync(w => w.UserId == userToken.UserId);

            if (total > 20) throw new Exception("超出购物车最大容量");


            var shopCartItem = req.Adapt<ShoppingCartItem>();
            shopCartItem.UserId = userToken.UserId;
            shopCartItem.CreateTime = DateTime.UtcNow;
            shopCartItem.UpdateTime = DateTime.UtcNow;

            context.ShoppingCartItems.Add(shopCartItem);
            await context.SaveChangesAsync();



        }

        public async Task UpdateMallCartItem(string token, UpdateCartItemParam req)
        {
            if (req.GoodsCount < 5) throw new Exception("超出单个商品最大购买数量！");

            var userToken = await context.UserTokens.
                SingleAsync(t => t.Token == token);

            var cartItem = await context.ShoppingCartItems.
                SingleOrDefaultAsync(u => u.CartItemId == req.CartItemId);

            if (cartItem == null) throw new Exception("未查询到记录");

            if (userToken.UserId != cartItem.UserId) throw new Exception("禁止该操作");

            cartItem.GoodsCount = req.GoodsCount;
            cartItem.UpdateTime = DateTime.Now;

            await context.SaveChangesAsync();


        }
    }
}
