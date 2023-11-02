using MallDomain.entity.common.response;
using Microsoft.AspNetCore.Mvc;

namespace MallApi.Controllers.mall {
    [ApiController]
    [Route("api/v1")]
    public class MallUserAddressController : ControllerBase {

        [HttpGet("address")]
        public async Task<Result> UserInfoUpdate() {
            return Result.Ok();
        }
        [HttpPost("address")]
        public async Task<Result> SaveUserAddress() {
            return Result.Ok();
        }
        [HttpPut("address")]
        public async Task<Result> UpdateMallUserAddress() {
            return Result.Ok();
        }
        [HttpGet("address/{addressId}")]
        public async Task<Result> GetMallUserAddress() {
            return Result.Ok();
        }
        [HttpGet("address/default")]
        public async Task<Result> GetMallUserDefaultAddress() {
            return Result.Ok();
        }
        [HttpDelete("address/{addressId}")]
        public async Task<Result> DeleteUserAddress() {
            return Result.Ok();
        }
    }
}
