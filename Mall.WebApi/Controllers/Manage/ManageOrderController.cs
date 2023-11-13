using Mall.Common.Result;
using Mall.Services;
using Mall.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace MallApi.Controllers.mannage
{
    [Route("manage-api/v1")]
    [ApiController]
    public class ManageOrderController : ControllerBase
    {
        private readonly ManageOrderService orderService;

        public ManageOrderController(ManageOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpPut("orders/checkDone")]
        public async Task<AppResult> CheckDoneOrder([FromBody] IdsReq ids)
        {
            await orderService.Ch_eckDone(ids.Ids);
            return AppResult.OkWithMessage("更新成功");
        }
        [HttpPut("orders/checkOut")]
        public async Task<AppResult> CheckOutOrder([FromBody] IdsReq ids)
        {
            await orderService.CheckOut(ids.Ids);
            return AppResult.OkWithMessage("更新成功");
        }
        [HttpPut("orders/close")]
        public async Task<AppResult> CloseOrder([FromBody] IdsReq ids)
        {
            await orderService.CloseOrder(ids.Ids);
            return AppResult.OkWithMessage("更新成功");

        }
        [HttpGet("orders/{orderId}")]
        public async Task<AppResult> FindMallOrder(long orderId)
        {
            var order = await orderService.GetMallOrder(orderId);
            return AppResult.OkWithData(order);

        }
        [HttpGet("orders")]
        public async Task<AppResult> GetMallOrderList([FromQuery] PageInfo info, [FromQuery] string? orderNo = "", [FromQuery] string? orderStatus = "")
        {
            var (list, total) = await orderService.GetMallOrderInfoList(info, orderNo, orderStatus);
            return AppResult.OkWithDetailed(new PageResult()
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
