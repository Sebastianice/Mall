using Mall.Common.Result;
using Mall.Services;
using Mall.Services.Models;
using MallDomain.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MallApi.Controllers.mall
{
    [ApiController]
    [Route("api/v1")]

    [Authorize(policy: "User")]
    public class MallShopCartController : ControllerBase
    {
        private readonly MallShopCartService mallShopCartService;

        public MallShopCartController(MallShopCartService mallShopCartService)
        {
            this.mallShopCartService = mallShopCartService;
        }

        [HttpGet("shop-cart")]
        public async Task<AppResult> CartItemList()
        {
            var token = Request.Headers["Authorization"]!;
            var shopCartItem = await mallShopCartService.GetMyShoppingCartItems(token!);

            return AppResult.OkWithData(shopCartItem);
        }
        [HttpPost("shop-cart")]
        public async Task<AppResult> SaveMallShoppingCartItem([FromBody] SaveCartItemParam saveCartItemParam)
        {
            var token = Request.Headers["Authorization"]!;
            await mallShopCartService.SaveMallCartItem(token!, saveCartItemParam);


            return AppResult.OkWithMessage("添加购物车成功");
        }


        [HttpPut("shop-cart")]
        public async Task<AppResult> UpdateMallShoppingCartItem([FromBody] UpdateCartItemParam req)
        {
            var token = Request.Headers["Authorization"]!;
            await mallShopCartService.UpdateMallCartItem(token!, req);
            return AppResult.OkWithMessage("修改购物车成功");
        }


        [HttpDelete("shop-cart/{newBeeMallShoppingCartItemId}")]
        public async Task<AppResult> DelMallShoppingCartItem(long shoppingCartItemId)
        {
            var token = Request.Headers["Authorization"]!;
            await mallShopCartService.DeleteMallCartItem(token!, shoppingCartItemId);
            return AppResult.OkWithMessage("删除成功");
        }


        [HttpGet("shop-cart/settle")]
        public async Task<AppResult> ToSettle([FromQuery] string cartItemIds)
        {
            var cartItemId = NumUtils.StrToInt(cartItemIds);
            var token = Request.Headers["Authorization"]!;

            var list = await mallShopCartService.GetCartItemsForSettle(token!, cartItemId);
            if (list.Count == 0)
            {
                return AppResult.FailWithMessage("获取购物车明细异常");
            }

            return AppResult.OkWithData(list);
        }
    }
}
