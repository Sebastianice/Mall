using MallDomain.entity.common.response;
using Microsoft.AspNetCore.Mvc;

namespace MallApi.Controllers.mannage
{
    [Route("manage-api/v1")]
    [ApiController]
    public class ManageIndexConfigController : ControllerBase
    {
        [HttpPost("indexConfigs")]
        public async Task<Result> CreateIndexConfig()
        {
            return Result.Ok();
        }
        [HttpPost("indexConfigs/delete")]
        public async Task<Result> DeleteIndexConfig()
        {
            return Result.Ok();
        }
        [HttpPut("indexConfigs")]
        public async Task<Result> UpdateIndexConfig()
        {
            return Result.Ok();
        }
        [HttpGet("indexConfigs/{id}")]
        public async Task<Result> FindIndexConfig()
        {
            return Result.Ok();
        }
        [HttpGet("indexConfigs")]
        public async Task<Result> GetIndexConfigList()
        {
            return Result.Ok();
        }
    }
}
