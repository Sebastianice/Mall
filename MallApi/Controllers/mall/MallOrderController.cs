using FluentValidation;
using MallDomain.entity.common.response;
using MallDomain.entity.mall.request;
using MallDomain.utils;
using Microsoft.AspNetCore.Mvc;

namespace MallApi.Controllers.mall {

    [ApiController]
    [Route("v1/api")]
    public class MallOrderController : ControllerBase {
        [HttpPost("saveOrder")]
        public async Task<Result> SaveOrder([FromBody]SaveOrderParam saverOrderParam) {
            if(saverOrderParam is null) {
                return Result.FailWithMessage("传值为空");
            }
            IValidator<SaveOrderParam> validator= ValidatorFactory.CreateValidator(saverOrderParam)!;
            var vResult=await validator.ValidateAsync(saverOrderParam);
            if (!vResult.IsValid) {
                return Result.FailWithMessage(vResult.Errors.ToString()!);
            }
            return Result.Ok();
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
