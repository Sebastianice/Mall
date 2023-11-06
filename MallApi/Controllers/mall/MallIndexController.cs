using MallDomain.entity.common.enums;
using MallDomain.entity.common.response;
using MallDomain.service.mall;
using Microsoft.AspNetCore.Mvc;

namespace MallApi.Controllers.mall
{
    [Route("api/v1")]
    [ApiController]
    public class MallIndexController : ControllerBase
    {
        private readonly IMallCarouselService mallCarouselService;



        private readonly IMallIndexInfoService mallIndexInfoService;

        public MallIndexController(IMallCarouselService mallCarouselService, IMallIndexInfoService mallIndexInfoService)
        {
            this.mallCarouselService = mallCarouselService;
            this.mallIndexInfoService = mallIndexInfoService;
        }


        [HttpGet("index-infos")]
        public async Task<Result> MallIndexInfo()
        {
            var mallCarouseInfo = await mallCarouselService.GetCarouselsForIndex(5);
            if (mallCarouseInfo.Count == 0)
            {
                Result.FailWithMessage("获取轮播图失败");

            }
            var hotGoodses = await mallIndexInfoService.GetConfigGoodsForIndex(IndexConfigEnum.IndexGoodsHot.Code(), 4);
            if (hotGoodses.Count == 0)
            {
                Result.FailWithMessage("热销商品获取失败");
            }
            var newGoodses = await mallIndexInfoService.GetConfigGoodsForIndex(IndexConfigEnum.IndexGoodsHot.Code(), 5);
            if (newGoodses.Count == 0)
            {
                Result.FailWithMessage("新品获取失败");
            }
            var recommendGoodses = await mallIndexInfoService.GetConfigGoodsForIndex(IndexConfigEnum.IndexGoodsHot.Code(), 10);
            if (recommendGoodses.Count == 0)
            {
                Result.FailWithMessage("推荐商品获取失败");
            }
            var indexResult = new Dictionary<string, Object>();
            indexResult["carousels"] = mallCarouseInfo;

            indexResult["hotGoodses"] = hotGoodses;

            indexResult["newGoodses"] = newGoodses;

            indexResult["recommendGoodses"] = recommendGoodses;
            return Result.OkWithData(indexResult);
        }
    }
}
