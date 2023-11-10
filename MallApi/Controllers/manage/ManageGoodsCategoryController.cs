using System.Text;
using MallDomain.entity.common.enums;
using MallDomain.entity.common.request;
using MallDomain.entity.common.response;
using MallDomain.entity.mannage;
using MallDomain.entity.mannage.request;
using MallDomain.service.manage;
using MallDomain.utils;
using MallDomain.utils.validator;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Crypto;

namespace MallApi.Controllers.mannage
{

    [Route("manage-api/v1")]
    [ApiController]
    public class ManageGoodsCategoryController:ControllerBase
    {
        private readonly IManageGoodsCategoryService manageGoodsCategoryService;

        public ManageGoodsCategoryController(IManageGoodsCategoryService manageGoodsCategoryService)
        {
            this.manageGoodsCategoryService = manageGoodsCategoryService;
        }

        [HttpPost("categories")]
        public async Task<Result> CreateCategory([FromBody] GoodsCategoryReq req)
        {
            var rs = await ValidatorFactory.CreateValidator(req)!.ValidateAsync(req);

            if (!rs.IsValid)
            {
                return Result.FailWithMessage("参数不合法");
            }
            await manageGoodsCategoryService.AddCategory(req);

            return Result.OkWithMessage("创建成功");
        }
        [HttpPut("categories")]
        public async Task<Result> UpdateCategory([FromBody] GoodsCategoryReq req)
        {
            var rs = await ValidatorFactory.CreateValidator(req)!.ValidateAsync(req);

            if (!rs.IsValid)
            {
                return Result.FailWithMessage("参数不合法");
            }

            await manageGoodsCategoryService.UpdateCategory(req);
            return Result.OkWithMessage("更新成功");
        }
        [HttpGet("categories")]
        public async Task<Result> GetCategoryList([FromQuery]PageInfo info,int categoryLevel, int parentId)
        {
            var (list, total) = await manageGoodsCategoryService.SelectCategoryPage( info, categoryLevel, parentId);
            return Result.OkWithDetailed(new PageResult()
            {
                List = list,
                CurrPage = info.PageNumber,
                TotalCount = total,
                PageSize = info.PageSize,
                 TotalPage = (int)Math.Ceiling((double)total / info.PageSize)
            }, "获取成功");

        }
        
        [HttpGet("categories/{id}")]
        public async Task<Result> GetCategory(long id)
        {
            var cg = await manageGoodsCategoryService.SelectCategoryById(id);

            return Result.OkWithData(cg);
        }
  
        [HttpDelete("categories")]
        
        public async Task<Result> DelCategory([FromBody]IdsReq ids)
        {          
            
            // DelCategory 设置分类失效
          
                await manageGoodsCategoryService.DeleteGoodsCategoriesByIds(ids.Ids);
  

            return Result.OkWithMessage("删除成功");
        }
    
        [HttpGet("categories4Select")]
        public async Task<Result> ListForSelect([FromQuery] long id)
        {
            // ListForSelect 用于三级分类联动效果制作
            var cat = await manageGoodsCategoryService.SelectCategoryById(id);
            var level = cat.CategoryLevel;
            if (level == GoodsCategoryLevel.LevelThree.Code()
                ||
               level == GoodsCategoryLevel.Default.Code())
            {
                return Result.FailWithMessage("参数异常");
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

            return Result.OkWithData(categoryResult);
        }
    }
   
}
