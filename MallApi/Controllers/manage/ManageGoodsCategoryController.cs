using MallDomain.entity.common.response;
using MallDomain.entity.mannage.request;
using MallDomain.service.mall;
using MallDomain.service.manage;
using MallDomain.utils.validator;
using MallInfrastructure.service.mall;
using Microsoft.AspNetCore.Mvc;

namespace MallApi.Controllers.mannage
{

    [Route("manage-api/v1")]
    [ApiController]
    public class ManageGoodsCategoryController
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
        public async Task<Result> GetCategoryList(SearchCategoryParams req)
        {
            var (list, total) = await manageGoodsCategoryService.SelectCategoryPage(req);
            return Result.OkWithDetailed(new PageResult()
            {
                List = list,
                CurrPage = req.PageInfo.PageNumber,
                TotalCount = total,
                PageSize = req.PageInfo.PageSize

            }, "获取成功");

        }
        [HttpGet("categories/{id}")]
        public async Task<Result> GetCategory(long id)
        {
            var cg = await manageGoodsCategoryService.SelectCategoryById(id);

            return Result.OkWithData(cg);
        }
        [HttpDelete("categories")]
        public async Task<Result> DelCategory(List<long> ids)
        {
            // DelCategory 设置分类失效
            await manageGoodsCategoryService.DeleteGoodsCategoriesByIds(ids);

            return Result.OkWithMessage("删除成功");
        }
        [HttpGet("categories4Select")]
        public async Task<Result> ListForSelect([FromQuery] long id)
        {
            // ListForSelect 用于三级分类联动效果制作
            var cat = await manageGoodsCategoryService.SelectCategoryById(id);
            ///TODO
            return Result.Ok();
        }
    }
}
