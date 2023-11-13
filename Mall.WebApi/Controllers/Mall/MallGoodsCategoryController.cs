using Mall.Common.Result;
using Mall.Services;
using Microsoft.AspNetCore.Mvc;

namespace MallApi.Controllers.mall
{


    [Route("api/v1")]
    [ApiController]
    public class MallGoodsCategoryController : ControllerBase
    {

        private readonly MallGoodsCategoryService mallGoodsCategoryService;
        public MallGoodsCategoryController(MallGoodsCategoryService mallGoodsCategoryService)
        {
            this.mallGoodsCategoryService = mallGoodsCategoryService;
        }

        [HttpGet("categories")]
        public async Task<AppResult> GetGoodsCategory()
        {
            var list = await mallGoodsCategoryService.GetCategoriesForIndex();
            if (list.Count <= 0)
            {
                return AppResult.FailWithMessage("查询失败");
            }
            return AppResult.OkWithData(list);
        }


    }
}
