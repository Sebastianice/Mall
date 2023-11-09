using MallDomain.entity.common.response;
using MallDomain.entity.mall.request;
using MallDomain.service.mall;
using MallDomain.utils.validator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MallApi.Controllers.mall
{
    [ApiController]
    [Route("api/v1")]

    [Authorize(policy: "User")]
    public class MallUserController : ControllerBase
    {
        private readonly IMallUserService mallUserService;
        private readonly IMallUserTokenService mallUserTokenService;
        public MallUserController(IMallUserService mallUserService, IMallUserTokenService mallUserTokenService)
        {
            this.mallUserService = mallUserService;
            this.mallUserTokenService = mallUserTokenService;
        }



        [HttpPut("user/info")]
        public async Task<Result> UserInfoUpdate([FromBody] UpdateUserInfoParam up)
        {
            var token = Request.Headers["Authorization"];

            await mallUserService.UpdateUserInfo(token, up);

            return Result.OkWithMessage("更新用户数据成功");
        }


        [HttpGet("user/info")]
        public async Task<Result> GetUserInfo()
        {
            var token = Request.Headers["Authorization"];

            var rep = await mallUserService.GetUserDetail(token);


            return Result.OkWithData(rep);
        }

        [HttpPost("user/logout")]
        public async Task<Result> UserLogout()
        {
            var token = Request.Headers["Authorization"];

            await mallUserTokenService.DeleteMallUserToken(token);

            return Result.OkWithMessage("登出成功");
        }
        [AllowAnonymous]
        [HttpPost("user/register")]
        public async Task<Result> UserRegister([FromBody] RegisterUserParam registerUser)
        {

            var vr = await ValidatorFactory.
                CreateValidator(registerUser)!.
                ValidateAsync(registerUser);

            if (!vr.IsValid)
            {
                var msg = string.Join(";", vr.Errors.Select(s => s.ErrorMessage));
                return Result.FailWithMessage(msg);
            }

            await mallUserService.RegisterUser(registerUser);
            return Result.OkWithMessage("注册成功");
        }
        [AllowAnonymous]
        [HttpPost("user/login")]
        public async Task<Result> UserLogin([FromBody] UserLoginParam userLoginParam)
        {

            var vr = await ValidatorFactory.
                           CreateValidator(userLoginParam)!.
                           ValidateAsync(userLoginParam);


            if (!vr.IsValid)
            {
                var msg =
                        string.
                        Join(";", vr.
                                       Errors.
                                       Select(s => s.ErrorMessage));


                return Result.FailWithMessage(msg);
            }


            var mallUserToken = await mallUserService.UserLogin(userLoginParam);

            return Result.OkWithData(mallUserToken.Token);
        }
    }
}
