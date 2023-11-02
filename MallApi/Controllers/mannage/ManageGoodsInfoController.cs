using MallDomain.entity.common.response;
using Microsoft.AspNetCore.Mvc;

namespace MallApi.Controllers.mannage {
    [Route("manage-api/v1")]
    [ApiController]
    public class ManageGoodsInfoController : ControllerBase {
        [HttpPost("goods")]
        public async Task<Result> CreateGoodsInfo() {
            return Result.Ok();
        }
        [HttpDelete("deleteMallGoodsInfo")]
        public async Task<Result> DeleteGoodsInfo() {
            return Result.Ok();
        }
        [HttpPut("goods/status/{status}")]
        public async Task<Result> ChangeGoodsInfoByIds() {
            return Result.Ok();
        }
        [HttpPut("goods")]
        public async Task<Result> UpdateGoodsInfo() {
            return Result.Ok();
        }
        [HttpGet("goods/{id}")]
        public async Task<Result> FindGoodsInfo() {
            return Result.Ok();
        }
        [HttpGet("goods/list")]
        public async Task<Result> GetGoodsInfoList() {
            return Result.Ok();
        }
    }
}
