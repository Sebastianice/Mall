using Mall.Common.Result;
using Mall.Repository.Enums;
using Mall.Repository.Models;
using Mall.Services;
using Mall.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace MallApi.Controllers.mannage
{

    [Route("manage-api/v1")]
    [ApiController]
    public class ManageGoodsCategoryController : ControllerBase
    {
        private readonly ManageGoodsCategoryService manageGoodsCategoryService;

        public ManageGoodsCategoryController(ManageGoodsCategoryService manageGoodsCategoryService)
        {
            this.manageGoodsCategoryService = manageGoodsCategoryService;
        }

        [HttpPost("categories")]
        public async Task<AppResult> CreateCategory([FromBody] GoodsCategoryReq req)
        {
            /* var rs = await ValidatorFactory.CreateValidator(req)!.ValidateAsync(req);

             if (!rs.IsValid)
             {
                 return AppResult.FailWithMessage("参数不合法");
             }*/
            await manageGoodsCategoryService.AddCategory(req);

            return AppResult.OkWithMessage("创建成功");
        }
        [HttpPut("categories")]
        public async Task<AppResult> UpdateCategory([FromBody] GoodsCategoryReq req)
        {
            /*  var rs = await ValidatorFactory.CreateValidator(req)!.ValidateAsync(req);

              if (!rs.IsValid)
              {
                  return AppResult.FailWithMessage("参数不合法");
              }*/

            await manageGoodsCategoryService.UpdateCategory(req);
            return AppResult.OkWithMessage("更新成功");
        }
        [HttpGet("categories")]
        public async Task<AppResult> GetCategoryList([FromQuery] PageInfo info, int categoryLevel, int parentId)
        {
            var (list, total) = await manageGoodsCategoryService.SelectCategoryPage(info, categoryLevel, parentId);
            return AppResult.OkWithDetailed(new PageResult()
            {
                List = list,
                CurrPage = info.PageNumber,
                TotalCount = total,
                PageSize = info.PageSize,
                TotalPage = (int)Math.Ceiling((double)total / info.PageSize)
            }, "获取成功");

        }

        [HttpGet("categories/{id}")]
        public async Task<AppResult> GetCategory(long id)
        {
            var cg = await manageGoodsCategoryService.SelectCategoryById(id);

            return AppResult.OkWithData(cg);
        }

        [HttpDelete("categories")]

        public async Task<AppResult> DelCategory([FromBody] IdsReq ids)
        {

            // DelCategory 设置分类失效

            await manageGoodsCategoryService.DeleteGoodsCategoriesByIds(ids.Ids);


            return AppResult.OkWithMessage("删除成功");
        }

        [HttpGet("categories4Select")]
        public async Task<AppResult> ListForSelect([FromQuery] long id)
        {
            // ListForSelect 用于三级分类联动效果制作
            var cat = await manageGoodsCategoryService.SelectCategoryById(id);
            var level = cat.CategoryLevel;
            if (level == GoodsCategoryLevel.LevelThree.Code()
                ||
               level == GoodsCategoryLevel.Default.Code())
            {
                return AppResult.FailWithMessage("参数异常");
            }

            var categoryResult = new Dictionary<string, List<GoodsCategory>>();

            if (level == GoodsCategoryLevel.LevelOne.Code())
            {
                var levelTwoList = await manageGoodsCategoryService.SelectByLevelAndParentIdsAndNumber(id, GoodsCategoryLevel.LevelTwo.Code());



                if (levelTwoList.Count > 0)
                {
                    var levelThreeList = await manageGoodsCategoryService.SelectByLevelAndParentIdsAndNumber(levelTwoList[0].CategoryId, GoodsCategoryLevel.LevelThree.Code());
                    categoryResult["secondLevelCategories"] = levelTwoList;
                    categoryResult["thirdLevelCategories"] = levelThreeList;
                }
            }
            if (level == GoodsCategoryLevel.LevelTwo.Code())
            {
                var levelThreeList = await manageGoodsCategoryService.SelectByLevelAndParentIdsAndNumber(id, GoodsCategoryLevel.LevelThree.Code());

                categoryResult["thirdLevelCategories"] = levelThreeList;
            }

            return AppResult.OkWithData(categoryResult);
        }
    }

}
