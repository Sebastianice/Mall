using Mall.Common.Result;
using Mall.Services;
using Mall.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MallApi.Controllers.mall
{

    [ApiController]
    [Route("api/v1")]
    [Authorize(policy: "User")]
    public class MallOrderController : ControllerBase
    {

        private readonly MallShopCartService mallShopCartService;
        private readonly MallUserAddressService mallUserAddressService;
        private readonly MallOrderService mallOrderService;

        public MallOrderController(MallShopCartService mallShopCartService, MallUserAddressService mallUserAddressService, MallOrderService mallOrderService)
        {
            this.mallShopCartService = mallShopCartService;
            this.mallUserAddressService = mallUserAddressService;
            this.mallOrderService = mallOrderService;
        }

        [HttpPost("saveOrder")]
        public async Task<AppResult> SaveOrder([FromBody] SaveOrderParam saverOrderParam)
        {
            /*IValidator<SaveOrderParam> validator = ValidatorFactory.CreateValidator(saverOrderParam)!;
            var vResult = await validator.ValidateAsync(saverOrderParam);

            if (!vResult.IsValid)
            {
                return AppResult.FailWithMessage(vResult.Errors.ToString()!);
            }*/

            var token = Request.Headers["Authorization"]!;
            var list = await mallShopCartService.GetCartItemsForSettle(token!, saverOrderParam.CartItemIds!);

            if (list.Count == 0)
            {
                return AppResult.FailWithMessage("无数据");
            }

            var userAddress = await mallUserAddressService.GetMallUserDefaultAddress(token!);
            var orderNo = await mallOrderService.SaveOrder(token!, userAddress, list);

            return AppResult.OkWithData(orderNo);
        }
        [HttpGet("paySuccess")]
        public async Task<AppResult> PaySuccess([FromQuery] string orderNo
            , [FromQuery] sbyte payType)
        {
            await mallOrderService.PaySuccess(orderNo, payType);
            return AppResult.OkWithMessage("订单支付成功");
        }

        [HttpPut("order/{orderNo}/finish")]
        public async Task<AppResult> FinishOrder(string orderNo)
        {
            var token = Request.Headers["Authorization"]!;
            await mallOrderService.FinishOrder(token!, orderNo);
            return AppResult.OkWithMessage("订单完成");
        }


        [HttpPut("order/{orderNo}/cancel")]
        public async Task<AppResult> CancelOrder(string orderNo)
        {
            var token = Request.Headers["Authorization"]!;
            await mallOrderService.CancelOrder(token!, orderNo);
            return AppResult.OkWithMessage("订单取消成功");
        }
        [HttpGet("order/{orderNo}")]
        public async Task<AppResult> OrderDetailPage(string orderNo)
        {
            var token = Request.Headers["Authorization"]!;
            var detail = await mallOrderService.GetOrderDetailByOrderNo(token!, orderNo);
            return AppResult.OkWithData(detail);

        }
        [HttpGet("order")]
        public async Task<AppResult> OrderList([FromQuery] int pageNumber, [FromQuery] string? status = "")
        {
            var token = Request.Headers["Authorization"]!;
            if (pageNumber <= 0)
            {
                pageNumber = 1;
            }

            var (list, total) = await mallOrderService.MallOrderListBySearch(token!, pageNumber, status);

            return AppResult.OkWithData(new PageResult()
            {
                PageSize = 5,
                TotalPage = (int)total,
                List = list,
            });
        }
    }
}
