using MallDomain.entity.common.request;
using MallDomain.entity.common.response;
using MallDomain.entity.mannage;
using MallDomain.entity.mannage.request;
using MallDomain.service.manage;
using MallDomain.utils.validator;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Ocsp;

namespace MallApi.Controllers.mannage
{
    [Route("manage-api/v1")]
    [ApiController]
    public class ManageGoodsInfoController : ControllerBase
    {
        private readonly IManageGoodsInfoService manageGoodsInfoService;
        public ManageGoodsInfoController(IManageGoodsInfoService manageGoodsInfoService)
        {
            this.manageGoodsInfoService = manageGoodsInfoService;
        }

        [HttpPost("goods")]
        public async Task<Result> CreateGoodsInfo([FromBody] GoodsInfoAddParam req)
        {
            var rs = await ValidatorFactory.CreateValidator(req)!.ValidateAsync(req);

            if (!rs.IsValid)
            {
                return Result.FailWithMessage("参数不合法");
            }
            await manageGoodsInfoService.CreateMallGoodsInfo(req);


            return Result.OkWithMessage("创建成功");
        }
        [HttpDelete("deleteMallGoodsInfo")]
        public async Task<Result> DeleteGoodsInfo([FromBody] GoodsInfo req)
        {
            await manageGoodsInfoService.DeleteMallGoodsInfo(req);
            return Result.OkWithMessage("删除成功");
        }
        [HttpPut("goods/status/{status}")]
        public async Task<Result> ChangeGoodsInfoByIds([FromBody]IdsReq ids, [FromQuery] string status)
        {
            await manageGoodsInfoService.ChangeMallGoodsInfoByIds(ids.Ids, status);
            return Result.OkWithMessage("修改商品状态成功");
        }
        [HttpPut("goods")]
        public async Task<Result> UpdateGoodsInfo([FromBody] GoodsInfoUpdateParam req)
        {
            var rs = await ValidatorFactory.CreateValidator(req)!.ValidateAsync(req);

            if (!rs.IsValid)
            {
                return Result.FailWithMessage("参数不合法");
            }
            await manageGoodsInfoService.UpdateMallGoodsInfo(req);

            return Result.OkWithMessage("修改商品成功");
        }
        [HttpGet("goods/{id}")]
        public async Task<Result> FindGoodsInfo(long id)
        {
            var info = await manageGoodsInfoService.GetMallGoodsInfo(id);
            return Result.OkWithData(info);
        }
        [HttpGet("goods/list")]
        public async Task<Result> GetGoodsInfoList([FromQuery] PageInfo pageInfo,
          [FromQuery] string? goodsName = "", [FromQuery] string? goodsSellStatus = "")
        {

            var (list, total) = await manageGoodsInfoService.GetMallGoodsInfoInfoList(pageInfo, goodsName!, goodsSellStatus!);

            return Result.OkWithDetailed(new PageResult()
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
