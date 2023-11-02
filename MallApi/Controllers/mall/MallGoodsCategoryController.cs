using MallDomain.entity.common.response;
using MallDomain.service.mall;
using Microsoft.AspNetCore.Mvc;

namespace MallApi.Controllers.mall {


    [Route("api/v1")]
    [ApiController]
    public class MallGoodsCategoryController : ControllerBase {

        private readonly IMallGoodsCategoryService mallGoodsCategoryService;
        public MallGoodsCategoryController(IMallGoodsCategoryService mallGoodsCategoryService) {
            this.mallGoodsCategoryService = mallGoodsCategoryService;
        }

        [HttpGet("categories")]
        public async Task<Result> GetGoodsCategory() {
            var list = await mallGoodsCategoryService.GetCategoriesForIndex();
            if (list.Count <= 0) {
                return Result.FailWithMessage("查询失败");
            }
            return Result.OkWithData(list);
        }


    }
}
