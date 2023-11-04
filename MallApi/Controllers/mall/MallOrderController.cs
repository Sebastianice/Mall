using FluentValidation;
using MallDomain.entity.common.response;
using MallDomain.entity.mall.request;
using MallDomain.service.mall;
using MallDomain.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MallApi.Controllers.mall {

    [ApiController]
    [Route("api/v1")]
    [Authorize(policy:"UserPolicy")]
    public class MallOrderController : ControllerBase {

        private readonly IMallShopCartService mallShopCartService;
        private readonly IMallUserAddressService mallUserAddressService;
        private readonly IMallOrderService mallOrderService;
        public MallOrderController(IMallShopCartService mallShopCartService, IMallUserAddressService uas, IMallOrderService ms) {
            this.mallShopCartService = mallShopCartService;
            this.mallUserAddressService = uas;
            this.mallOrderService = ms;
        }

        [HttpPost("saveOrder")]
        public async Task<Result> SaveOrder([FromBody]SaveOrderParam saverOrderParam) {
         

            IValidator<SaveOrderParam> validator= ValidatorFactory.CreateValidator(saverOrderParam)!;
            var vResult=await validator.ValidateAsync(saverOrderParam);
            if (!vResult.IsValid) {
                return Result.FailWithMessage(vResult.Errors.ToString()!);
            }
            var token = Request.Headers["Authorization"].ToString()[7..];
        var list    =await mallShopCartService.GetCartItemsForSettle(token, saverOrderParam.CartItemIds!);
            if (list.Count == 0) {
                return Result.FailWithMessage("无数据");
            }
            
            var userAddress = await mallUserAddressService.GetMallUserDefaultAddress(token);
          var orderNo=await  mallOrderService.SaveOrder(token, userAddress, list);

            return Result.OkWithData(orderNo);
        }
        [HttpGet("paySuccess")]
        public async Task<Result> PaySuccess() {
            return Result.Ok();
        }
        [HttpPut("order/{orderNo}/finish")]
        public async Task<Result> FinishOrder(long orderNo) {
            return Result.Ok();
        }
        [HttpPut("order/{orderNo}/cancel")]
        public async Task<Result> CancelOrder(long orderNo) {
            return Result.Ok();
        }
        [HttpGet("order/{orderNo}")]
        public async Task<Result> OrderDetailPage(long orderNo) {
            return Result.Ok();
        }
        [HttpGet("order")]
        public async Task<Result> OrderList(long orderNo) {
            return Result.Ok();
        }


    }
}
