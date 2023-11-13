using Mall.Common.Result;
using Mall.Services;
using Mall.Services.Models;
using Microsoft.AspNetCore.Mvc;



namespace MallApi.Controllers.mall
{
    [Route("api/v1")]
    [ApiController]
    public class MallGoodsInfoController : ControllerBase
    {
        private readonly MallGoodsInfoService mallGoodsInfoService;

        public MallGoodsInfoController(MallGoodsInfoService mallGoodsInfoService)
        {
            this.mallGoodsInfoService = mallGoodsInfoService;
        }

        [HttpGet("search")]
        public async Task<AppResult> GoodsSearch(
          [FromQuery] int pageNumber, [FromQuery] int goodsCategoryId, [FromQuery] string? keyword = "", [FromQuery] string? orderBy = "")
        {
            (var list, var total) = await mallGoodsInfoService.MallGoodsListBySearch
                (pageNumber, goodsCategoryId, keyword, orderBy);



            return AppResult.OkWithDetailed(new PageResult()
            {
                List = list,
                CurrPage = pageNumber,
                TotalCount = total,
                PageSize = 10,
                TotalPage = (int)Math.Ceiling(total / 10.0),
            }, "获取成功");
        }


        [HttpGet("goods/detail/{id}")]
        public async Task<AppResult> GoodsDetail(long id)
        {
            var data = await mallGoodsInfoService.GetMallGoodsInfo(id);

            return AppResult.OkWithData(data);
        }



    }
}
