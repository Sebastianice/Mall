using LinqKit;
using MallDomain.entity.common.enums;
using MallDomain.entity.common.request;

using MallDomain.entity.mannage;
using MallDomain.entity.mannage.response;
using MallDomain.service.manage;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace MallInfrastructure.service.mannage
{
    public class ManageOrderService : IManageOrderService
    {
        private readonly MallContext context;

        public ManageOrderService(MallContext context)
        {
            this.context = context;
        }
        // CheckDone 修改订单状态为配货成功
        public async Task Ch_eckDone(List<long> ids)
        {

            var orders = await context.Orders
                 .Where(w => ids.Contains(w.OrderId))
                 .ToListAsync();
            if (orders.Count() == 0) throw new Exception("未查询到订单");

            //订单存在未支付
            bool flag = false;

            foreach (var order in orders)
            {
                if (order.OrderStatus == OrderStatusEnum.ORDER_PAID.Code())
                    flag = true;
            }

            if (flag) throw new Exception("存在未支付订单，不能配货");



            await context.Orders
                .Where(i => ids.Contains(i.OrderId))
                .ExecuteUpdateAsync(s => s
                .SetProperty(p => p.OrderStatus, 2)
                .SetProperty(p => p.UpdateTime, DateTime.Now));
        }

        // CheckOut 出库
        public async Task CheckOut(List<long> ids)
        {

            var orders = await context.Orders
                 .Where(w => ids.Contains(w.OrderId))
                 .ToListAsync();
            if (orders.Count() == 0) throw new Exception("未查询到订单");




            //订单存在未支付
            bool flag = false;

            foreach (var order in orders)
            {
                if (order.OrderStatus == OrderStatusEnum.ORDER_PAID.Code())
                    if (order.OrderStatus == OrderStatusEnum.ORDER_PACKAGED.Code())
                        flag = true;
            }

            if (flag) throw new Exception("未支付或未配货状态，不能出库");



            await context.Orders
                .Where(i => ids.Contains(i.OrderId))
                .ExecuteUpdateAsync(s => s
                .SetProperty(p => p.OrderStatus, 3)
                .SetProperty(p => p.UpdateTime, DateTime.Now));
        }


        public Task CloseOrder(List<long> ids)
        {
            throw new NotImplementedException();
        }

        public async Task<NewBeeMallOrderDetailVO> GetMallOrder(long id)
        {

            var order = await context.Orders
                 .SingleOrDefaultAsync(w => w.OrderId == id)
                 ?? throw new Exception("未查询到订单");

            //获取订单下的条目
            var orderItems = await context.OrderItems
                         .Where(w => w.OrderId == order.OrderId)
                         .AsNoTracking()
                         .ToListAsync();

            if (orderItems.Count() == 0) throw new Exception("订单异常");

            var newBeeMallOrderDetailVO = order.Adapt<NewBeeMallOrderDetailVO>();
            newBeeMallOrderDetailVO.NewBeeMallOrderItemVOS = orderItems.Adapt<List<NewBeeMallOrderItemVO>>();

            string statusStr = MallOrderStatusExtensions.GetNewBeeMallOrderStatusEnumByStatus(order.OrderStatus);
            string payTapStr = MallOrderStatusExtensions.GetNewBeeMallOrderStatusEnumByStatus(order.PayStatus);
            newBeeMallOrderDetailVO.OrderStatusString = statusStr;
            newBeeMallOrderDetailVO.PayTypeString = payTapStr;

            return newBeeMallOrderDetailVO;
        }

        public async Task<(List<Order>, long)> GetMallOrderInfoList(PageInfo info, string orderNo, string orderStatus)
        {
            var limit = info.PageSize;
            var offset = limit * (info.PageNumber - 1);


            var query = context.Orders.AsQueryable();

            if (!string.IsNullOrEmpty(orderNo))
            {
                query = query.Where(w => w.OrderNo == orderNo);
            }

            if (!string.IsNullOrEmpty(orderStatus))
            {
                var status = int.Parse(orderStatus);
                query = query.Where(w => w.OrderStatus == status);
            }

            
                              

            var count = await query.CountAsync();

            var list = await query.Skip(offset).Take(limit).ToListAsync();

            return (list, count);
        }
    }
}
