using Mall.Common.Result;
using Mall.Services;
using Mall.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MallApi.Controllers.mall
{
    [ApiController]
    [Route("api/v1")]
    [Authorize(policy: "User")]
    public class MallUserController : ControllerBase
    {
        private readonly MallUserService mallUserService;
        private readonly MallUserTokenService mallUserTokenService;
        public MallUserController(MallUserService mallUserService, MallUserTokenService mallUserTokenService)
        {
            this.mallUserService = mallUserService;
            this.mallUserTokenService = mallUserTokenService;
        }



        [HttpPut("user/info")]
        public async Task<AppResult> UserInfoUpdate([FromBody] UpdateUserInfoParam up)
        {
            var token = Request.Headers["Authorization"]!;

            await mallUserService.UpdateUserInfo(token!, up);

            return AppResult.OkWithMessage("更新用户数据成功");
        }


        [HttpGet("user/info")]
        public async Task<AppResult> GetUserInfo()
        {
            var token = Request.Headers["Authorization"]!;

            var rep = await mallUserService.GetUserDetail(token!);


            return AppResult.OkWithData(rep);
        }

        [HttpPost("user/logout")]
        public async Task<AppResult> UserLogout()
        {
            string token = Request.Headers["Authorization"]!;

            await mallUserTokenService.DeleteMallUserToken(token);

            return AppResult.OkWithMessage("登出成功");
        }
        [AllowAnonymous]
        [HttpPost("user/register")]
        public async Task<AppResult> UserRegister([FromBody] RegisterUserParam registerUser)
        {

            await mallUserService.RegisterUser(registerUser);
            return AppResult.OkWithMessage("注册成功");
        }
        [AllowAnonymous]
        [HttpPost("user/login")]
        public async Task<AppResult> UserLogin([FromBody] UserLoginParam userLoginParam)
        {

            var mallUserToken = await mallUserService.UserLogin(userLoginParam);

            return AppResult.OkWithData(mallUserToken.Token);
        }
    }
}
