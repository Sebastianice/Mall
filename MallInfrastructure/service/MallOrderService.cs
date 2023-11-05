using System.Collections.Immutable;
using System.Collections.Specialized;
using MallDomain.entity.common.enums;
using MallDomain.entity.mall;
using MallDomain.entity.mall.response;
using MallDomain.entity.mannage;
using MallDomain.entity.mannage.request;
using MallDomain.service.mall;
using MallDomain.utils;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace MallInfrastructure.service {
    public class MallOrderService : IMallOrderService {
        private readonly MallContext mallContext;

        public MallOrderService(MallContext mallContext) {
            this.mallContext = mallContext;
        }
        // CancelOrder 关闭订单
        public async Task CancelOrder(string token, string orderNo) {
            var userToken = await mallContext.MallUserTokens.Where(p => p.Token == token).SingleOrDefaultAsync();
            if (userToken == null) {
                throw new Exception("不存在的用户");
            }
            var order = await mallContext.MallOrders.SingleOrDefaultAsync(i => i.OrderNo == orderNo && i.UserId == userToken.UserId);
            if (order == null) {
                throw new Exception("不存在订单");
            }
            order.OrderStatus = MallOrderStatusEnum.ORDER_CLOSED_BY_MALLUSER.Code();
            order.UpdateTime = DateTime.Now;
            await mallContext.SaveChangesAsync();
        }
        //完结订单
        public async Task FinishOrder(string token, string orderNo) {
            var userToken = await mallContext.MallUserTokens.Where(p => p.Token == token).SingleOrDefaultAsync();
            if (userToken == null) {
                throw new Exception("不存在的用户");
            }
            var order = await mallContext.MallOrders.SingleOrDefaultAsync(i => i.OrderNo == orderNo && i.UserId == userToken.UserId);
            if (order == null) {
                throw new Exception("不存在订单");
            }
            order.OrderStatus = MallOrderStatusEnum.ORDER_SUCCESS.Code();
            order.UpdateTime = DateTime.Now;
            await mallContext.SaveChangesAsync();
        }

        public async Task<MallOrderDetailVO> GetOrderDetailByOrderNo(string token, string orderNo) {
            var userToken = await mallContext.MallUserTokens.Where(p => p.Token == token).SingleOrDefaultAsync();
            if (userToken == null) {
                throw new Exception("不存在的用户");
            }
            MallOrder? order = await mallContext.MallOrders.SingleOrDefaultAsync(i => i.OrderNo == orderNo && i.UserId == userToken.UserId);
            if (order == null) {
                throw new Exception("不存在订单");
            }
            List<MallOrderItem>? orderItems = await mallContext.MallOrderItems.Where(w => w.OrderId == order.OrderId).ToListAsync();
            if (orderItems.Count() == 0) {
                throw new Exception("该订单项不存在");
            }
            List<NewBeeMallOrderItemVO> list = orderItems.Adapt<List<NewBeeMallOrderItemVO>>();
            MallOrderDetailVO? detail = order.Adapt<MallOrderDetailVO>();
            var orderStatusStr = MallOrderStatusExtensions.GetNewBeeMallOrderStatusEnumByStatus(detail.OrderStatus);
            var paystr = MallOrderStatusExtensions.GetNewBeeMallOrderStatusEnumByStatus(detail.PayType);
            detail.PayTypeString = paystr;
            detail.OrderStatusString = orderStatusStr;
            detail.NewBeeMallOrderItemVO = list;
            return detail;
        }

        public async Task<(List<MallOrderResponse> list, long total)> MallOrderListBySearch(string token, int pageNumber, string status) {
            var userToken = await mallContext.MallUserTokens.Where(p => p.Token == token).SingleOrDefaultAsync();
            if (userToken == null) {
                throw new Exception("不存在的用户");
            }
            var query = mallContext.MallOrders.AsQueryable();
            if (status != "") {
                var s = int.Parse(status);
                query = query.Where(w => w.OrderStatus == s);
            }
            var count = await query.Where(w => w.UserId == userToken.UserId).CountAsync();
            //这里前段没有做滚动加载，直接显示全部订单
            //limit=5;
            var offset = 5 * (pageNumber - 1);


            if (count > 0) {
                var orders =await query.OrderByDescending(o => o.OrderId).Skip(offset).Take(5).AsNoTracking().ToListAsync();
                //实体转换
                var orderResp = orders.Adapt<List<MallOrderResponse>>();
                foreach (var item in orderResp) {
                    //订单状态设置中文值显示
                    item.OrderStatusString = MallOrderStatusExtensions.GetNewBeeMallOrderStatusEnumByStatus(item.OrderStatus);
                }


                // 返回订单id
                List<long> ids = new();
                foreach (var item in orders)
                {
                    ids.Add(item.OrderId);
                }
              var items=await  mallContext.MallOrderItems.Where(i => ids.Contains(i.OrderId)).ToListAsync();

              var  itemByOrderIdMap=new Dictionary<long,List<MallOrderItem>>();
                foreach (var item in itemByOrderIdMap)
                {
                    item.Value = new List<MallOrderItem>();
                }


            }

            return (new List<MallOrderResponse>(), 0);
        }

        public async Task PaySuccess(string orderNo, int payType) {
            MallOrder? order = await mallContext.MallOrders.SingleAsync(s => s.OrderNo == orderNo);
            if (order.OrderStatus == 0) {
                throw new Exception("订单状态异常");
            }
            order.OrderStatus = MallOrderStatusEnum.ORDER_PAID.Code();
            order.PayType = payType;
            order.PayStatus = 1;
            order.PayTime = DateTime.Now;
            order.UpdateTime = DateTime.Now;

            await mallContext.SaveChangesAsync();
        }



        public async Task<string> SaveOrder(string token, MallUserAddress userAddress, List<CartItemResponse> myShoppingCartItems) {

            var userToken = await mallContext.MallUserTokens.Where(p => p.Token == token).SingleOrDefaultAsync();
            if (userToken == null) {
                throw new Exception("不存在的用户");
            }
            var itemIdlist = new List<long>();
            var goodsIds = new List<long>();
            foreach (var item in myShoppingCartItems) {
                itemIdlist.Add(item.CartItemId);
                goodsIds.Add(item.GoodsId);
            }

            var newBeeMallGoods = await mallContext.MallGoodsInfos.Where(g => goodsIds.Contains(g.GoodsId)).AsNoTracking().ToArrayAsync();

            //对把商品id和商品对应起来
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
                        //     mallContext.MallGoodsInfos.Update(goodsInfo);

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

                    await mallContext.MallOrders.AddAsync(newBeeMallOrder);
                    //保存更改获取订单主键
                    await mallContext.SaveChangesAsync();
                    //生成订单收货地址快照，并保存至数据库
                    var newBeeMallOrderAddress = userAddress.Adapt<MallOrderAddress>();
                    newBeeMallOrderAddress.OrderId = newBeeMallOrder.OrderId;
                    await mallContext.MallOrderAddressese.AddAsync(newBeeMallOrderAddress);
                    //生成所有的订单项快照，并保存至数据库
                    List<MallOrderItem> newBeeMallOrderItems = new List<MallOrderItem>();
                    foreach (var item in myShoppingCartItems) {
                        var newBeeMallOrderItem = item.Adapt<MallOrderItem>();
                        newBeeMallOrderItem.OrderId = newBeeMallOrder.OrderId;
                        newBeeMallOrderItem.CreateTime = DateTime.Now;
                        newBeeMallOrderItems.Add(newBeeMallOrderItem);

                    }
                    await mallContext.MallOrderItems.AddRangeAsync(newBeeMallOrderItems);

                    await mallContext.SaveChangesAsync();

                    return orderNo;
                }
            }

            throw new NotImplementedException();
        }
    }
}
