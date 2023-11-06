using MallApi.filter;
using MallDomain.entity.common.response;
using MallDomain.entity.mall.request;
using MallDomain.service.mall;
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
        private readonly IMallShopCartService mallShopCartService;

        public MallShopCartController(IMallShopCartService mallShopCartService)
        {
            this.mallShopCartService = mallShopCartService;
        }

        [HttpGet("shop-cart")]
        public async Task<Result> CartItemList()
        {
            var token = Request.Headers["Authorization"].ToString()[7..];
            var shopCartItem = await mallShopCartService.GetMyShoppingCartItems(token);
            if (shopCartItem.Count <= 0)
            {
                return Result.FailWithMessage("获取购物车失败");
            }
            return Result.OkWithData(shopCartItem);
        }
        [HttpPost("shop-cart")]
        public async Task<Result> SaveMallShoppingCartItem([FromBody] SaveCartItemParam saveCartItemParam)
        {
            var token = Request.Headers["Authorization"].ToString()[7..];
           await mallShopCartService.SaveMallCartItem(token, saveCartItemParam);


            return Result.OkWithMessage("添加购物车成功");
        }


        [HttpPut("shop-cart")]
        public async Task<Result> UpdateMallShoppingCartItem([FromBody] UpdateCartItemParam req)
        {
            var token = Request.Headers["Authorization"].ToString()[7..];
            await mallShopCartService.UpdateMallCartItem(token, req);
            return Result.OkWithMessage("修改购物车成功");
        }


        [HttpDelete("shop-cart/{newBeeMallShoppingCartItemId}")]
        public async Task<Result> DelMallShoppingCartItem(long shoppingCartItemId)
        {
            var token = Request.Headers["Authorization"].ToString()[7..];
            await mallShopCartService.DeleteMallCartItem(token, shoppingCartItemId);
            return Result.OkWithMessage("删除成功");
        }


        [HttpGet("shop-cart/settle")]
        public async Task<Result> ToSettle([FromQuery] string cartItemId)
        {
            var cartItemIds = NumUtils.StrToInt(cartItemId);
            var token = Request.Headers["Authorization"].ToString()[7..];

            var list = await mallShopCartService.GetCartItemsForSettle(token, cartItemIds);
            if (list.Count == 0)
            {
                return Result.FailWithMessage("获取购物车明细异常");
            }

            return Result.OkWithData(list);
        }
    }
}
