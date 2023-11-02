using MallDomain.entity.common.response;
using Microsoft.AspNetCore.Mvc;

namespace MallApi.Controllers.mannage {
    [Route("manage-api/v1")]
    [ApiController]
    public class ManageOrderController : ControllerBase {
        [HttpPut("orders/checkDone")]
        public async Task<Result> CheckDoneOrder() {
            return Result.Ok();
        }
        [HttpPut("orders/checkOut")]
        public async Task<Result> CheckOutOrder() {
            return Result.Ok();
        }
        [HttpPut("orders/close")]
        public async Task<Result> CloseOrder() {
            return Result.Ok();
        }
        [HttpGet("orders/{orderId}")]
        public async Task<Result> FindMallOrder() {
            return Result.Ok();
        }
        [HttpGet("orders")]
        public async Task<Result> GetMallOrderList() {
            return Result.Ok();
        }
    }
}
