using MallDomain.entity.common.response;
using Microsoft.AspNetCore.Mvc;

namespace MallApi.Controllers.mall {
    [ApiController]
    [Route("api/v1")]
    public class MallShopCartController : ControllerBase {
        [HttpGet("shop-cart")]
        public async Task<Result> CartItemList() {
            return Result.Ok();
        }
        [HttpPost("shop-cart")]
        public async Task<Result> SaveMallShoppingCartItem() {
            return Result.Ok();
        }
        [HttpPut("shop-cart")]
        public async Task<Result> UpdateMallShoppingCartItem() {
            return Result.Ok();
        }
        [HttpDelete("shop-cart/{newBeeMallShoppingCartItemId}")]
        public async Task<Result> DelMallShoppingCartItem(long newBeeMallShoppingCartItemId) {
            return Result.Ok();
        }
        [HttpGet("shop-cart/settle")]
        public async Task<Result> ToSettle() {
            return Result.Ok();
        }
    }
}
