using FluentValidation;
using MallDomain.entity.common.response;
using MallDomain.entity.mall.request;
using MallDomain.service.mall;
using MallDomain.utils.validator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MallApi.Controllers.mall
{

    [ApiController]
    [Route("api/v1")]
    [Authorize(policy: "User")]
    public class MallOrderController : ControllerBase
    {

        private readonly IMallShopCartService mallShopCartService;
        private readonly IMallUserAddressService mallUserAddressService;
        private readonly IMallOrderService mallOrderService;

        public MallOrderController(IMallShopCartService mallShopCartService, IMallUserAddressService uas, IMallOrderService ms)
        {
            this.mallShopCartService = mallShopCartService;
            this.mallUserAddressService = uas;
            this.mallOrderService = ms;
        }

        [HttpPost("saveOrder")]
        public async Task<Result> SaveOrder([FromBody] SaveOrderParam saverOrderParam)
        {
            IValidator<SaveOrderParam> validator = ValidatorFactory.CreateValidator(saverOrderParam)!;
            var vResult = await validator.ValidateAsync(saverOrderParam);

            if (!vResult.IsValid)
            {
                return Result.FailWithMessage(vResult.Errors.ToString()!);
            }

            var token = Request.Headers["Authorization"];
            var list = await mallShopCartService.GetCartItemsForSettle(token, saverOrderParam.CartItemIds!);

            if (list.Count == 0)
            {
                return Result.FailWithMessage("无数据");
            }

            var userAddress = await mallUserAddressService.GetMallUserDefaultAddress(token);
            var orderNo = await mallOrderService.SaveOrder(token, userAddress, list);

            return Result.OkWithData(orderNo);
        }
        [HttpGet("paySuccess")]
        public async Task<Result> PaySuccess([FromQuery] string orderNo
            , [FromQuery] sbyte payType)
        {
            await mallOrderService.PaySuccess(orderNo, payType);
            return Result.OkWithMessage("订单支付成功");
        }

        [HttpPut("order/{orderNo}/finish")]
        public async Task<Result> FinishOrder(string orderNo)
        {
            var token = Request.Headers["Authorization"];
            await mallOrderService.FinishOrder(token, orderNo);
            return Result.OkWithMessage("订单完成");
        }


        [HttpPut("order/{orderNo}/cancel")]
        public async Task<Result> CancelOrder(string orderNo)
        {
            var token = Request.Headers["Authorization"];
            await mallOrderService.CancelOrder(token, orderNo);
            return Result.OkWithMessage("订单取消成功");
        }
        [HttpGet("order/{orderNo}")]
        public async Task<Result> OrderDetailPage(string orderNo)
        {
            var token = Request.Headers["Authorization"];
            var detail = await mallOrderService.GetOrderDetailByOrderNo(token, orderNo);
            return Result.OkWithData(detail);

        }
        [HttpGet("order")]
        public async Task<Result> OrderList( [FromQuery] int pageNumber, [FromQuery] string? status="")
        {
            var token = Request.Headers["Authorization"];
            if (pageNumber <= 0)
            {
                pageNumber = 1;
            }

            var (list, total) = await mallOrderService.MallOrderListBySearch(token, pageNumber, status);

            return Result.OkWithData(new PageResult()
            {
                PageSize = 5,
                TotalPage = (int)total,
                List = list,
            });
        }
    }
}
