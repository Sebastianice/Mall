using MallDomain.entity.common.request;
using MallDomain.entity.common.response;
using MallDomain.service.manage;
using Microsoft.AspNetCore.Mvc;

namespace MallApi.Controllers.mannage
{
    [Route("manage-api/v1")]
    [ApiController]
    public class ManageOrderController : ControllerBase
    {
        private readonly IManageOrderService orderService;

        public ManageOrderController(IManageOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpPut("orders/checkDone")]
        public async Task<Result> CheckDoneOrder([FromBody] IdsReq ids)
        {
            await orderService.Ch_eckDone(ids.Ids);
            return Result.OkWithMessage("更新成功");
        }
        [HttpPut("orders/checkOut")]
        public async Task<Result> CheckOutOrder([FromBody] IdsReq ids)
        {
            await orderService.CheckOut(ids.Ids);
            return Result.OkWithMessage("更新成功");
        }
        [HttpPut("orders/close")]
        public async Task<Result> CloseOrder([FromBody] IdsReq ids)
        {
            await orderService.CloseOrder(ids.Ids);
            return Result.OkWithMessage("更新成功");

        }
        [HttpGet("orders/{orderId}")]
        public async Task<Result> FindMallOrder(long orderId)
        {
            var order = await orderService.GetMallOrder(orderId);
            return Result.OkWithData(order);

        }
        [HttpGet("orders")]
        public async Task<Result> GetMallOrderList([FromQuery] PageInfo info, [FromQuery] string? orderNo = "", [FromQuery] string? orderStatus = "")
        {
            var (list, total) = await orderService.GetMallOrderInfoList(info, orderNo, orderStatus);
            return Result.OkWithDetailed(new PageResult()
            {
                List = list,
                CurrPage = info.PageNumber,
                TotalCount = total,
                PageSize = info.PageSize,
                TotalPage = (int)Math.Ceiling((double)total / info.PageSize)

            }, "获取成功");

        }
    }
}
