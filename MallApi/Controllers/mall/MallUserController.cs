using System.Linq;
using MallDomain.entity.common.response;
using MallDomain.entity.mall.request;
using MallDomain.service.mall;
using MallDomain.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MallApi.Controllers.mall {
    [ApiController]
    [Route("api/v1")]
    [Authorize]
    public class MallUserController : ControllerBase {
        private readonly IMallUserService mallUserService;

        public MallUserController(IMallUserService mallUserService) {
            this.mallUserService = mallUserService;
        }

        [HttpPut("user/info")]
        public async Task<Result> UserInfoUpdate() {
            return Result.Ok();
        }
        [HttpGet("user/info")]
        public async Task<Result> GetUserInfo() {
            return Result.Ok();
        }
        [HttpPost("user/logout")]
        public async Task<Result> UserLogout() {
            return Result.Ok();
        }
        [AllowAnonymous]
        [HttpPost("user/register")]
        public async Task<Result> UserRegister([FromBody] RegisterUserParam registerUser) {
            if (registerUser is null) {

                return Result.FailWithMessage("对象为null");
            }
            var vr = await ValidatorFactory.CreateValidator
 (registerUser)!.ValidateAsync(registerUser);
            if (!vr.IsValid) {
                var msg = string.Join(";", vr.Errors.Select(s => s.ErrorMessage));
                return Result.FailWithMessage(msg);
            }

            await mallUserService.RegisterUser(registerUser);
            return Result.OkWithMessage("注册成功");
        }
        [AllowAnonymous]
        [HttpPost("user/login")]
        public async Task<Result> UserLogin([FromBody] UserLoginParam userLoginParam) {
            if (userLoginParam is null) {

                return Result.FailWithMessage("对象为null");
            }
            var vr = await ValidatorFactory.CreateValidator
 (userLoginParam)!.ValidateAsync(userLoginParam);
            if (!vr.IsValid) {
                return Result.FailWithMessage(vr.Errors.ToString()!);
            }
           var mallUserToken= await mallUserService.UserLogin(userLoginParam);
            return Result.OkWithDetailed(mallUserToken,"登录成功");
        }
    }
}
