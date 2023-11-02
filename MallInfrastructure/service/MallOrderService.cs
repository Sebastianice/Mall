using MallDomain.entity.common.enums;
using MallDomain.entity.mall;
using MallDomain.entity.mall.response;
using MallDomain.entity.mannage;
using MallDomain.entity.mannage.request;
using MallDomain.service.mall;
using MallDomain.utils;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace MallInfrastructure.service {
    public class MallOrderService : IMallOrderService {
        private readonly MallContext mallContext;

        public MallOrderService(MallContext mallContext) {
            this.mallContext = mallContext;
        }

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



        public async Task<string> SaveOrder(string token, MallUserAddress userAddress, List<CartItemResponse> myShoppingCartItems) {

            var userToken = await mallContext.MallUserTokens.Where(p => p.Token == token).SingleOrDefaultAsync();
            if (userToken == null) {
                throw new Exception("不存在的用户");
            }
            var itemIdlist = new List<long> ();
            var goodsIds = new List<long>();
            foreach(var item in myShoppingCartItems) {
                itemIdlist.Add(item.CartItemId);
                goodsIds.Add(item.GoodsId);
            }

            var newBeeMallGoods = await mallContext.MallGoodsInfos.Where(g => goodsIds.Contains(g.GoodsId)).AsNoTracking().ToArrayAsync();
            var newBeeMallGoodsMap = new Dictionary<long, MallGoodsInfo>();
            foreach (var item in newBeeMallGoods) {
                if (item.GoodsSellStatus == GoodsStatusEnum.GOODS_UNDER.Code()) {
                    throw new Exception("商品已经下架，无法生成订单");
                }
                newBeeMallGoodsMap[item.GoodsId] = item;

            }

            //判断商品库存
            foreach (var item in myShoppingCartItems) {

                if (newBeeMallGoodsMap.TryGetValue(item.GoodsId, out var goodsInfo)) {
                    if (item.GoodsCount > goodsInfo.StockNum) {
                        throw new Exception("库存不足");
                    }
                } else {
                    //查出的商品中不存在购物车中的这条关联商品数据，直接返回错误提醒
                    throw new Exception("购物车数据异常");
                }

            }
            //删除购物项
            if (itemIdlist.Count() > 0 && goodsIds.Count() > 0) {
                /*   var shoppingCartItems = await mallContext.MallShoppingCartItems.Where(i => itemIdlist.Contains(i.CartItemId)).ToArrayAsync();*/

                //var shoppingCartItems = new List<MallShoppingCartItem>();

                /*  foreach (var item in itemIdlist) {
                      shoppingCartItems.Add(new MallShoppingCartItem() {
                          IsDeleted = true,
                          CartItemId=item
                      });                  
                  }*/
                //使用ef core 新特性批量操作
                using (var transaction = mallContext.Database.BeginTransaction()) {
                    await mallContext.MallShoppingCartItems.Where(u => itemIdlist.Contains(u.CartItemId)).
                    ExecuteUpdateAsync(set => set.SetProperty(p => p.IsDeleted, true));
                    /* mallContext.MallShoppingCartItems.AttachRange(shoppingCartItems);
                     mallContext.MallShoppingCartItems.Entry(shoppingCartItems)
                     mallContext.MallShoppingCartItems.UpdateRange(shoppingCartItems);*/


                    var stockNumDTOS = myShoppingCartItems.Adapt<List<StockNumDTO>>();
                    foreach (var item in stockNumDTOS) {
                        var goodsInfo = await mallContext.MallGoodsInfos.
                            Where(w => item.GoodsId == w.GoodsId &&
                            w.StockNum > item.GoodsCount && w.GoodsSellStatus == 0).SingleOrDefaultAsync();
                        if (goodsInfo is null) {
                            throw new Exception("库存不足");
                        }
                        goodsInfo.StockNum = goodsInfo.StockNum - item.GoodsCount;
                        mallContext.MallGoodsInfos.Update(goodsInfo);

                    }

                    var orderNo = NumUtils.GenOrderNo();
                    var newBeeMallOrder = new MallOrder() {
                        OrderNo = orderNo,
                        UserId = userToken.UserId
                    };

                    //总价
                    var priceTotal = 0;
                    foreach (var item in myShoppingCartItems) {
                        priceTotal = priceTotal + item.GoodsCount * item.SellingPrice;
                    }
                    if (priceTotal <= 0) {
                        throw new Exception("订单价格异常");
                    }

                    newBeeMallOrder.CreateTime = DateTime.Now;
                    newBeeMallOrder.UpdateTime = DateTime.Now;
                    newBeeMallOrder.TotalPrice = priceTotal;
                    newBeeMallOrder.ExtraInfo = "";

                    //生成订单项并保存订单项纪录
                    
             await  mallContext.MallOrders.AddAsync(newBeeMallOrder);
                    //保存更改获取订单主键
             await mallContext.SaveChangesAsync();
                    //生成订单收货地址快照，并保存至数据库
                    var newBeeMallOrderAddress = userAddress.Adapt<MallOrderAddress>();
                    newBeeMallOrderAddress.OrderId = newBeeMallOrder.OrderId;
                    await mallContext.MallOrderAddressese.AddAsync(newBeeMallOrderAddress);
                    //生成所有的订单项快照，并保存至数据库
                    List<MallOrderItem> newBeeMallOrderItems = new List<MallOrderItem>();
                    foreach (var item in myShoppingCartItems)
                    {
                        var newBeeMallOrderItem = item.Adapt<MallOrderItem>();
                        newBeeMallOrderItem.OrderId = newBeeMallOrder.OrderId;
                        newBeeMallOrderItem.CreateTime = DateTime.Now;
                        newBeeMallOrderItems.Add(newBeeMallOrderItem);

                    }
                  await  mallContext.MallOrderItems.AddRangeAsync(newBeeMallOrderItems);

                    await mallContext.SaveChangesAsync();
                }
            }

            throw new NotImplementedException();
        }
    }
}
