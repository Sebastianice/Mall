﻿using MallDomain.entity.common.request;
using MallDomain.entity.common.response;
using MallDomain.entity.mannage;
using MallDomain.entity.mannage.request;
using MallDomain.service.manage;
using MallDomain.utils.validator;
using MallInfrastructure.service.mannage;
using Microsoft.AspNetCore.Mvc;

namespace MallApi.Controllers.mannage
{
    [Route("manage-api/v1")]
    [ApiController]
    public class ManageIndexConfigController : ControllerBase
    {
        private readonly IManageIndexConfigService indexConfigService;

        public ManageIndexConfigController(IManageIndexConfigService indexConfigService)
        {
            this.indexConfigService = indexConfigService;
        }

        [HttpPost("indexConfigs")]
        public async Task<Result> CreateIndexConfig([FromBody] IndexConfigAddParams req)
        {
            var rs = await ValidatorFactory.CreateValidator(req)!.ValidateAsync(req);

            if (!rs.IsValid)
            {
                return Result.FailWithMessage("参数不合法");
            }
            await indexConfigService.CreateMallIndexConfig(req);

            return Result.OkWithMessage("创建成功");
        }

        [HttpPost("indexConfigs/delete")]
        public async Task<Result> DeleteIndexConfig([FromBody]IdsReq ids)
        {
            await indexConfigService.DeleteMallIndexConfig(ids.Ids);

             return Result.OkWithMessage("删除成功");
        }
        [HttpPut("indexConfigs")]
        public async Task<Result> UpdateIndexConfig([FromBody] IndexConfigUpdateParams req)
        {
            var rs = await ValidatorFactory.CreateValidator(req)!.ValidateAsync(req);

            if (!rs.IsValid)
            {
                return Result.FailWithMessage("参数不合法");
            }
            await indexConfigService.UpdateMallIndexConfig(req);

            return Result.OkWithMessage("更新成功");
        }
        [HttpGet("indexConfigs/{id}")]
        public async Task<Result> FindIndexConfig(long id)
        {
            var config = await indexConfigService.GetMallIndexConfig(id);
            return Result.OkWithData(config);
        }


        [HttpGet("indexConfigs")]
        public async Task<Result> GetIndexConfigList([FromQuery] PageInfo info,[FromQuery] sbyte configType)
        {
            var (list, total) = await indexConfigService.GetMallIndexConfigInfoList(info,  configType);

             return Result.OkWithDetailed(new PageResult()
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
