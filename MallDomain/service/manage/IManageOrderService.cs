using MallDomain.entity.common.request;
using MallDomain.entity.mannage;
using MallDomain.entity.mannage.response;

namespace MallDomain.service.manage
{
    public interface IManageOrderService
    {
        // CheckDone 修改订单状态为配货成功
        public Task Ch_eckDone(List<long> ids);

        // CheckOut 出库
        public Task CheckOut(List<long> ids);

        // CloseOrder 商家关闭订单
        public Task CloseOrder(List<long> ids);

        // GetMallOrder 根据id获取MallOrder记录
        public Task<NewBeeMallOrderDetailVO> GetMallOrder(long id);
        // GetMallOrderInfoList 分页获取MallOrder记录
        public Task<(List<Order>, long)> GetMallOrderInfoList(PageInfo info, string orderNo, string orderStatus);
    }
}
