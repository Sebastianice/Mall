using MallDomain.entity.common.response;
using MallDomain.service.mall;
using Microsoft.AspNetCore.Mvc;



namespace MallApi.Controllers.mall
{
    [Route("api/v1")]
    [ApiController]
    public class MallGoodsInfoController : ControllerBase
    {
        private readonly IMallGoodsInfoService mallGoodsInfoService;

        public MallGoodsInfoController(IMallGoodsInfoService mallGoodsInfoService)
        {
            this.mallGoodsInfoService = mallGoodsInfoService;
        }

        [HttpGet("search")]
        public async Task<Result> GoodsSearch(
           [FromQuery] int pageNumber, [FromQuery] int goodsCategoryId, [FromQuery] string keyword, [FromQuery] string orderBy
            )
        {
            (var list, var total) = await mallGoodsInfoService.MallGoodsListBySearch(pageNumber, goodsCategoryId, keyword, orderBy);
            if (list.Count == 0)
            {
                return Result.FailWithMessage("获取失败");
            }
            return Result.OkWithDetailed(new PageResult()
            {
                List = list,
                CurrPage = pageNumber,
                TotalCount = total,
                PageSize = 10,
                TotalPage = (int)Math.Ceiling(total / 10.0),
            }, "获取成功");
        }
        [HttpGet("goods/detail/{id}")]
        public async Task<Result> GoodsDetail(long id)
        {
            var data = await mallGoodsInfoService.GetMallGoodsInfo(id);
            if (data is null)
            {
                return Result.FailWithMessage("获取数据失败");
            }

            return Result.OkWithData(data);
        }



    }
}
