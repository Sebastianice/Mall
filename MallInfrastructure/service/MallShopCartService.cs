using MallDomain.entity.mall;
using MallDomain.entity.mall.request;
using MallDomain.entity.mall.response;
using MallDomain.entity.mannage;
using MallDomain.service.mall;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace MallInfrastructure.service
{
    public class MallShopCartService : IMallShopCartService
    {

        private readonly MallContext context;
        private MallGoodsInfo _;

        public MallShopCartService(MallContext mallContext)
        {
            this.context = mallContext;
        }

        public async Task DeleteMallCartItem(string token, long id)
        {
            MallUserToken? userToken = await context.MallUserTokens.SingleOrDefaultAsync(t => t.Token == token);
            if (userToken is null)
            {
                throw new Exception("不存在用户");
            }
            var item = context.MallShoppingCartItems.SingleOrDefault(u => u.UserId == userToken.UserId && u.CartItemId == id);
            if (item is null)
            {
                throw new Exception("没有相应记录");
            }
            item.IsDeleted = true;
            await context.SaveChangesAsync();
        }


        public async Task<List<CartItemResponse>> GetCartItemsForSettle(string token, List<long> cartItemIds)
        {
            var v = await context.MallUserTokens.SingleOrDefaultAsync(t => t.Token == token);
            if (v is null)
            {
                throw new Exception("不存在用户");
            }
            MallShoppingCartItem[]? shopCartItems = await context.MallShoppingCartItems.
                Where(w => w.UserId == v.UserId && cartItemIds.Contains(w.CartItemId)).AsNoTracking().ToArrayAsync();
            List<CartItemResponse>? cartItemsRes = await getMallShoppingCartItemVOS(shopCartItems);
            return cartItemsRes;
        }


        //购物车转换
        private async Task<List<CartItemResponse>> getMallShoppingCartItemVOS(MallShoppingCartItem[] cartItems)
        {
            List<long> ids = new List<long>();
            foreach (var item in cartItems)
            {
                ids.Add(item.GoodsId);
            }
            var newBeeMallGoods = await context.MallGoodsInfos.Where(g => ids.Contains(g.GoodsId)).AsNoTracking().ToArrayAsync();

            var newBeeMallGoodsMap = new Dictionary<long, MallGoodsInfo>();
            foreach (var item in newBeeMallGoods)
            {
                newBeeMallGoodsMap[item.GoodsId] = item;
            }
            List<CartItemResponse> cartItemsRes = new();
            foreach (MallShoppingCartItem item in cartItems)
            {
                CartItemResponse? cartItemRes = item.Adapt<CartItemResponse>();
                if (newBeeMallGoodsMap.TryGetValue(cartItemRes.GoodsId, out MallGoodsInfo? v))
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
            var userToken = await context.MallUserTokens.SingleAsync(t => t.Token == token);
            var shopcarts = await context.MallShoppingCartItems.Where(u => u.UserId == userToken.UserId).AsNoTracking().ToListAsync();
            List<long> goodsIds = new List<long>();
            foreach (var item in shopcarts)
            {
                goodsIds.Add(item.GoodsId);
            }
            var goodInfos = context.MallGoodsInfos.Where(w => goodsIds.Contains(w.GoodsId)).AsNoTracking();
            Dictionary<long, MallGoodsInfo> goodsMap = new();
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

        public async Task<bool> SaveMallCartItem(string token, SaveCartItemParam req)
        {
            if (req.GoodsCount < 1) throw new Exception("商品数量不能小于1");
            if (req.GoodsCount < 5) throw new Exception("超出单个商品最大购买数量！");


            var userToken = await context.MallUserTokens.
                AsNoTracking().
                SingleAsync(t => t.Token == token);
            //判断是否存在商品
            var shopItem = await context.MallShoppingCartItems.Where(w => w.UserId == userToken.UserId && w.GoodsId == req.intGoodsId).AsNoTracking().FirstOrDefaultAsync();
            if (shopItem != null)
            {
                throw new Exception("商品已存在！无需重复添加");
            }
            var goodInfo = context.MallGoodsInfos.FirstOrDefault(w => w.GoodsId == req.intGoodsId);
            if (goodInfo == null)
            {
                throw new Exception("商品为空");
            }
            var total = await context.MallShoppingCartItems.CountAsync(w => w.UserId == userToken.UserId);
            if (total > 20) throw new Exception("超出购物车最大容量");
            var shopCartItem = req.Adapt<MallShoppingCartItem>();
            shopCartItem.UserId = userToken.UserId;
            shopCartItem.CreateTime = DateTime.UtcNow;
            shopCartItem.UpdateTime = DateTime.UtcNow;
            context.MallShoppingCartItems.Add(shopCartItem);
            await context.SaveChangesAsync();
            return true;

        }

        public async Task UpdateMallCartItem(string token, UpdateCartItemParam req)
        {
            if (req.GoodsCount < 5) throw new Exception("超出单个商品最大购买数量！");
            var userToken = await context.MallUserTokens.SingleAsync(t => t.Token == token);

            var cartItem = await context.MallShoppingCartItems.SingleOrDefaultAsync(u => u.CartItemId == req.CartItemId);
            if (cartItem == null)
            {
                throw new Exception("未查询到记录");
            }
            if (userToken.UserId != cartItem.UserId)
            {
                throw new Exception("禁止该操作");
            }
            cartItem.GoodsCount = req.GoodsCount;
            cartItem.UpdateTime = DateTime.Now;
            await context.SaveChangesAsync();


        }
    }
}
