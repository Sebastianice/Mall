
using Mall.Common.Result;
using Mall.Repository;
using Mall.Repository.Enums;
using Mall.Repository.Models;
using Mall.Services.Models;
using MallDomain.utils;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Mall.Services
{
    public class MallOrderService
    {
        private readonly MallContext mallContext;

        public MallOrderService(MallContext mallContext)
        {
            this.mallContext = mallContext;
        }
        // CancelOrder 关闭订单
        public async Task CancelOrder(string token, string orderNo)
        {
            var userToken = await mallContext.UserTokens.
                Where(p => p.Token == token).
                SingleOrDefaultAsync();

            if (userToken == null) throw ResultException.FailWithMessage("不存在的用户");

            var order = await mallContext.Orders
                .SingleOrDefaultAsync(i => i.OrderNo == orderNo && i.UserId == userToken.UserId);

            if (order == null) throw ResultException.FailWithMessage("不存在订单");

            order.OrderStatus = OrderStatusEnum.ORDER_CLOSED_BY_MALLUSER.Code();
            order.UpdateTime = DateTime.Now;
            await mallContext.SaveChangesAsync();
        }
        //完结订单
        public async Task FinishOrder(string token, string orderNo)
        {
            var userToken = await mallContext.UserTokens.
                Where(p => p.Token == token).
                SingleOrDefaultAsync();

            if (userToken == null) throw ResultException.FailWithMessage("不存在的用户");

            var order = await mallContext.Orders.
                SingleOrDefaultAsync(i => i.OrderNo == orderNo && i.UserId == userToken.UserId);

            if (order == null) throw ResultException.FailWithMessage("不存在订单");

            order.OrderStatus = OrderStatusEnum.ORDER_SUCCESS.Code();
            order.UpdateTime = DateTime.Now;

            await mallContext.SaveChangesAsync();
        }

        public async Task<MallOrderDetailVO> GetOrderDetailByOrderNo(string token, string orderNo)
        {
            var userToken = await mallContext.UserTokens.
               SingleOrDefaultAsync(p => p.Token == token);

            if (userToken == null) throw ResultException.FailWithMessage("不存在的用户");

            Order? order = await mallContext.Orders.
                SingleOrDefaultAsync(i => i.OrderNo == orderNo && i.UserId == userToken.UserId);

            if (order == null) throw ResultException.FailWithMessage("不存在订单");

            List<OrderItem>? orderItems = await mallContext.OrderItems.
                Where(w => w.OrderId == order.OrderId).
                ToListAsync();

            if (orderItems.Count() == 0) throw ResultException.FailWithMessage("该订单项不存在");

            List<NewBeeMallOrderItemVO> list = orderItems.Adapt<List<NewBeeMallOrderItemVO>>();
            MallOrderDetailVO? detail = order.Adapt<MallOrderDetailVO>();

            var orderStatusStr = MallOrderStatusExtensions.
                GetNewBeeMallOrderStatusEnumByStatus(detail.OrderStatus);
            var paystr = MallOrderStatusExtensions.
                GetNewBeeMallOrderStatusEnumByStatus(detail.PayType);

            detail.PayTypeString = paystr;
            detail.OrderStatusString = orderStatusStr;
            detail.NewBeeMallOrderItemVOS = list;

            return detail;
        }

        public async Task<(List<MallOrderResponse> list, long total)> MallOrderListBySearch(string token, int pageNumber, string? status)
        {
            var userToken = await mallContext.UserTokens.Where(p => p.Token == token).SingleOrDefaultAsync();
            if (userToken == null)
            {
                throw ResultException.FailWithMessage("不存在的用户");
            }
            var query = mallContext.Orders.AsQueryable();
            if (status != null && status != "")
            {
                var s = int.Parse(status);
                query = query.Where(w => w.OrderStatus == s);
            }
            var count = await query.Where(w => w.UserId == userToken.UserId).CountAsync();
            //这里前段没有做滚动加载，直接显示全部订单
            //limit=5;
            var offset = 5 * (pageNumber - 1);


            if (count > 0)
            {
                var orders = await query.OrderByDescending(o => o.OrderId).Skip(offset).Take(5).AsNoTracking().ToListAsync();
                //实体转换
                var orderResp = orders.Adapt<List<MallOrderResponse>>();

                //返回多个订单id 为每一个订单列表子项dto分配空间
                List<long> ids = new();
                foreach (var item in orderResp)
                {
                    //订单状态设置中文值显示
                    item.OrderStatusString = MallOrderStatusExtensions.GetNewBeeMallOrderStatusEnumByStatus(item.OrderStatus);

                    //id列表 查询用
                    ids.Add(item.OrderId);

                    //分配空间
                    item.NewBeeMallOrderItemVOS = new();

                }




                //通过多个订单id找到订单子项
                var items = await mallContext.OrderItems.Where(i => ids.Contains(i.OrderId)).ToListAsync();



                //循环全部订单的子项
                foreach (var item in items)
                {
                    //循环全部订单，如果订单id等于子项的id，把子项加到订单里
                    foreach (var or in orderResp)
                    {
                        if (or.OrderId == item.OrderId)
                            or.NewBeeMallOrderItemVOS!
                                .Add(item.Adapt<NewBeeMallOrderItemVO>());
                    }
                }

                return (orderResp, count);
            }

            return (new List<MallOrderResponse>(), 0);
        }

        public async Task PaySuccess(string orderNo, sbyte payType)
        {
            Order? order = await mallContext.Orders.
                SingleAsync(s => s.OrderNo == orderNo);

            if (order.OrderStatus != 0)
                throw ResultException.FailWithMessage("订单状态异常");

            order.OrderStatus = OrderStatusEnum.ORDER_PAID.Code();
            order.PayType = payType;
            order.PayStatus = 1;
            order.PayTime = DateTime.Now;
            order.UpdateTime = DateTime.Now;

            await mallContext.SaveChangesAsync();
        }



        public async Task<string> SaveOrder(string token, UserAddress userAddress, List<CartItemResponse> myShoppingCartItems)
        {

            var userToken = await mallContext.UserTokens.Where(p => p.Token == token).SingleOrDefaultAsync();
            if (userToken == null) throw ResultException.FailWithMessage("不存在的用户");

            var itemIdlist = new List<long>();
            var goodsIds = new List<long>();

            foreach (var item in myShoppingCartItems)
            {
                itemIdlist.Add(item.CartItemId);
                goodsIds.Add(item.GoodsId);
            }

            var newBeeMallGoods = await mallContext.GoodsInfos.
                Where(g => goodsIds.Contains(g.GoodsId)).
                AsNoTracking().
                ToArrayAsync();

            //对把商品id和商品对应起来
            var newBeeMallGoodsMap = new Dictionary<long, GoodsInfo>();
            foreach (var item in newBeeMallGoods)
            {
                if (item.GoodsSellStatus == GoodsStatusEnum.GOODS_UNDER.Code())
                {
                    throw ResultException.FailWithMessage("商品已经下架，无法生成订单");
                }

                newBeeMallGoodsMap[item.GoodsId] = item;
            }

            //判断商品库存
            foreach (var item in myShoppingCartItems)
            {
                if (newBeeMallGoodsMap.TryGetValue(item.GoodsId, out var goodsInfo))
                {
                    if (item.GoodsCount > goodsInfo.StockNum)
                    {
                        throw ResultException.FailWithMessage("库存不足");
                    }
                }
                else
                {
                    //查出的商品中不存在购物车中的这条关联商品数据，直接返回错误提醒
                    throw ResultException.FailWithMessage("购物车数据异常");
                }

            }


            //删除购物车项
            if (itemIdlist.Count() > 0 && goodsIds.Count() > 0)
            {
                //使用ef core 新特性批量操作
                using (var transaction = mallContext.Database.BeginTransaction())
                {
                    await mallContext.ShoppingCartItems.
                        Where(u => itemIdlist.Contains(u.CartItemId)).
                    ExecuteUpdateAsync(set => set.SetProperty(p => p.IsDeleted, 1));


                    var stockNumDTOS = myShoppingCartItems.Adapt<List<StockNumDTO>>();
                    foreach (var item in stockNumDTOS)
                    {
                        var goodsInfo = await mallContext.GoodsInfos.
                            Where(w => item.GoodsId == w.GoodsId &&
                            w.StockNum > item.GoodsCount && w.GoodsSellStatus == 0).
                            SingleOrDefaultAsync();

                        if (goodsInfo is null) throw ResultException.FailWithMessage("库存不足");

                        goodsInfo.StockNum = goodsInfo.StockNum - item.GoodsCount;

                        //     context.GoodsInfos.Update(goodsInfo);

                    }

                    var orderNo = NumUtils.GenOrderNo();
                    var newBeeMallOrder = new Order()
                    {
                        OrderNo = orderNo,
                        UserId = userToken.UserId
                    };

                    //总价
                    var priceTotal = 0;
                    foreach (var item in myShoppingCartItems)
                    {
                        priceTotal = priceTotal + item.GoodsCount * item.SellingPrice;
                    }

                    if (priceTotal <= 0) throw ResultException.FailWithMessage("订单价格异常");


                    newBeeMallOrder.CreateTime = DateTime.Now;
                    newBeeMallOrder.UpdateTime = DateTime.Now;
                    newBeeMallOrder.TotalPrice = priceTotal;
                    newBeeMallOrder.ExtraInfo = "";

                    //生成订单项并保存订单项纪录

                    await mallContext.Orders.AddAsync(newBeeMallOrder);

                    //保存更改获取订单主键
                    await mallContext.SaveChangesAsync();

                    //生成订单收货地址快照，并保存至数据库
                    var newBeeMallOrderAddress = userAddress.Adapt<OrderAddress>();
                    newBeeMallOrderAddress.OrderId = newBeeMallOrder.OrderId;
                    await mallContext.OrderAddressese.AddAsync(newBeeMallOrderAddress);


                    //生成所有的订单项快照，并保存至数据库
                    List<OrderItem> newBeeMallOrderItems = new List<OrderItem>();
                    foreach (var item in myShoppingCartItems)
                    {
                        var newBeeMallOrderItem = item.Adapt<OrderItem>();
                        newBeeMallOrderItem.OrderId = newBeeMallOrder.OrderId;
                        newBeeMallOrderItem.CreateTime = DateTime.Now;
                        newBeeMallOrderItems.Add(newBeeMallOrderItem);

                    }

                    await mallContext.OrderItems.AddRangeAsync(newBeeMallOrderItems);

                    await mallContext.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return orderNo;
                }
            }

            throw ResultException.FailWithMessage("购物车无数据，保存订单失败");
        }
    }
}
