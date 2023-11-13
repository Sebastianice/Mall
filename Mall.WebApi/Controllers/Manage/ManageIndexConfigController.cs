using Mall.Common.Result;
using Mall.Services;
using Mall.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace MallApi.Controllers.mannage
{
    [Route("manage-api/v1")]
    [ApiController]
    public class ManageIndexConfigController : ControllerBase
    {
        private readonly ManageIndexConfigService indexConfigService;

        public ManageIndexConfigController(ManageIndexConfigService indexConfigService)
        {
            this.indexConfigService = indexConfigService;
        }

        [HttpPost("indexConfigs")]
        public async Task<AppResult> CreateIndexConfig([FromBody] IndexConfigAddParams req)
        {
           /* var rs = await ValidatorFactory.CreateValidator(req)!.ValidateAsync(req);

            if (!rs.IsValid)
            {
                return AppResult.FailWithMessage("参数不合法");
            }*/
            await indexConfigService.CreateMallIndexConfig(req);

            return AppResult.OkWithMessage("创建成功");
        }

        [HttpDelete("indexConfigs")]
        public async Task<AppResult> DeleteIndexConfig([FromBody] IdsReq ids)
        {
            await indexConfigService.DeleteMallIndexConfig(ids.Ids);

            return AppResult.OkWithMessage("删除成功");
        }
        [HttpPut("indexConfigs")]
        public async Task<AppResult> UpdateIndexConfig([FromBody] IndexConfigUpdateParams req)
        {
           /* var rs = await ValidatorFactory.CreateValidator(req)!.ValidateAsync(req);

            if (!rs.IsValid)
            {
                return AppResult.FailWithMessage("参数不合法");
            }*/
            await indexConfigService.UpdateMallIndexConfig(req);

            return AppResult.OkWithMessage("更新成功");
        }
        [HttpGet("indexConfigs/{id}")]
        public async Task<AppResult> FindIndexConfig(long id)
        {
            var config = await indexConfigService.GetMallIndexConfig(id);
            return AppResult.OkWithData(config);
        }


        [HttpGet("indexConfigs")]
        public async Task<AppResult> GetIndexConfigList([FromQuery] PageInfo info, [FromQuery] sbyte configType)
        {
            var (list, total) = await indexConfigService.GetMallIndexConfigInfoList(info, configType);

            return AppResult.OkWithDetailed(new PageResult()
            {
                List = list,
                CurrPage = info.PageNumber,
                TotalCount = total,
                PageSize = info.PageSize,
                TotalPage = (int)Math.Ceiling((double)total / info.PageSize)
            }, "获取成功");
        }
    }
}
