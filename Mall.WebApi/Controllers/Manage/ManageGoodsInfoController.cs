using Mall.Common.Result;
using Mall.Repository.Models;
using Mall.Services;
using Mall.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace MallApi.Controllers.mannage
{
    [Route("manage-api/v1")]
    [ApiController]
    public class ManageGoodsInfoController : ControllerBase
    {
        private readonly ManageGoodsInfoService manageGoodsInfoService;
        public ManageGoodsInfoController(ManageGoodsInfoService manageGoodsInfoService)
        {
            this.manageGoodsInfoService = manageGoodsInfoService;
        }

        [HttpPost("goods")]
        public async Task<AppResult> CreateGoodsInfo([FromBody] GoodsInfoAddParam req)
        {
           /* var rs = await ValidatorFactory.CreateValidator(req)!.ValidateAsync(req);

            if (!rs.IsValid)
            {
                return AppResult.FailWithMessage("参数不合法");
            }*/
            await manageGoodsInfoService.CreateMallGoodsInfo(req);


            return AppResult.OkWithMessage("创建成功");
        }
        [HttpDelete("deleteMallGoodsInfo")]
        public async Task<AppResult> DeleteGoodsInfo([FromBody] GoodsInfo req)
        {
            await manageGoodsInfoService.DeleteMallGoodsInfo(req);
            return AppResult.OkWithMessage("删除成功");
        }
        [HttpPut("goods/status/{status}")]
        public async Task<AppResult> ChangeGoodsInfoByIds([FromBody] IdsReq ids, [FromQuery] string status)
        {
            await manageGoodsInfoService.ChangeMallGoodsInfoByIds(ids.Ids, status);
            return AppResult.OkWithMessage("修改商品状态成功");
        }
        [HttpPut("goods")]
        public async Task<AppResult> UpdateGoodsInfo([FromBody] GoodsInfoUpdateParam req)
        {
            /*var rs = await ValidatorFactory.CreateValidator(req)!.ValidateAsync(req);

            if (!rs.IsValid)
            {
                return AppResult.FailWithMessage("参数不合法");
            }*/
            await manageGoodsInfoService.UpdateMallGoodsInfo(req);

            return AppResult.OkWithMessage("修改商品成功");
        }
        [HttpGet("goods/{id}")]
        public async Task<AppResult> FindGoodsInfo(long id)
        {
            var info = await manageGoodsInfoService.GetMallGoodsInfo(id);
            return AppResult.OkWithData(info);
        }
        [HttpGet("goods/list")]
        public async Task<AppResult> GetGoodsInfoList([FromQuery] PageInfo pageInfo,
          [FromQuery] string? goodsName = "", [FromQuery] string? goodsSellStatus = "")
        {

            var (list, total) = await manageGoodsInfoService.GetMallGoodsInfoInfoList(pageInfo, goodsName!, goodsSellStatus!);

            return AppResult.OkWithDetailed(new PageResult()
            {
                List = list,
                CurrPage = pageInfo.PageNumber,
                TotalCount = total,
                PageSize = pageInfo.PageSize,
                TotalPage = (int)Math.Ceiling((double)total / pageInfo.PageSize)
            }, "获取成功");
        }
    }
}
