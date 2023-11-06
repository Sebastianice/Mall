using MallDomain.entity.common.response;
using Microsoft.AspNetCore.Mvc;

namespace MallApi.Controllers.mannage
{

    [Route("manage-api/v1")]
    [ApiController]
    public class ManageAdminUserController : ControllerBase
    {

        [HttpPost("createMallAdminUser")]
        public async Task<Result> CreateAdminUser()
        {
            return Result.Ok();
        }
        [HttpPut("adminUser/name")]
        public async Task<Result> UpdateAdminUserName()
        {
            return Result.Ok();
        }
        [HttpPut("adminUser/password")]
        public async Task<Result> UpdateAdminUserPassword()
        {
            return Result.Ok();
        }
        [HttpGet("users")]
        public async Task<Result> UserList()
        {
            return Result.Ok();
        }
        [HttpPut("users/{lockStatus}")]
        public async Task<Result> LockUser()
        {
            return Result.Ok();
        }
        [HttpGet("adminUser/profile")]
        public async Task<Result> AdminUserProfile()
        {
            return Result.Ok();
        }

        [HttpDelete("logout")]
        public async Task<Result> AdminLogout()
        {
            return Result.Ok();
        }
        [HttpPost("upload/file")]
        public async Task<Result> UploadFile()
        {
            return Result.Ok();
        }
        [HttpPost("adminUser/login")]
        public async Task<Result> AdminLogin()
        {
            return Result.Ok();
        }
    }
}
