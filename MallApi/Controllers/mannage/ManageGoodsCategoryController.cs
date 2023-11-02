using MallDomain.entity.common.response;
using Microsoft.AspNetCore.Mvc;

namespace MallApi.Controllers.mannage {
    [Route("manage-api/v1")]
    [ApiController]
    public class ManageGoodsCategoryController {

        [HttpPost("categories")]
        public async Task<Result> CreateCategory() {
            return Result.Ok();
        }
        [HttpPut("categories")]
        public async Task<Result> UpdateCategory() {
            return Result.Ok();
        }
        [HttpGet("categories")]
        public async Task<Result> GetCategoryList() {
            return Result.Ok();
        }
        [HttpGet("categories/{id}")]
        public async Task<Result> GetCategory() {
            return Result.Ok();
        }
        [HttpDelete("categories")]
        public async Task<Result> DelCategory() {
            return Result.Ok();
        }
        [HttpGet("categories4Select")]
        public async Task<Result> ListForSelect() {
            return Result.Ok();
        }
    }
}
