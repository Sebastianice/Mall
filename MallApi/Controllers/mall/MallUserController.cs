using MallApi.filter;
using MallDomain.entity.common.response;
using MallDomain.entity.mall.request;
using MallDomain.service.mall;
using MallDomain.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace MallApi.Controllers.mall {
    [ApiController]
    [Route("api/v1")]

    [Authorize(policy:"UserPolicy")]
    public class MallUserController : ControllerBase {
        private readonly IMallUserService mallUserService;
        private readonly IMallUserTokenService mallUserTokenService;
        public MallUserController(IMallUserService mallUserService, IMallUserTokenService mallUserTokenService) {
            this.mallUserService = mallUserService;
            this.mallUserTokenService= mallUserTokenService;
        }
        [ServiceFilter(typeof(TokenFilter))]
        [HttpPut("user/info")]
        public async Task<Result> UserInfoUpdate([FromBody]UpdateUserInfoParam up) {
            var token = Request.Headers["Authorization"].ToString()[7..];

        var rep= await mallUserService.UpdateUserInfo(token, up);
            if (!rep) {
                return Result.FailWithMessage("更新用户信息失败");
            }

            return Result.OkWithMessage("更新用户数据成功");
        }
        [ServiceFilter(typeof(TokenFilter))]
        [HttpGet("user/info")]
        public async Task<Result> GetUserInfo() {
            var token = Request.Headers["Authorization"].ToString()[7..];
       var rep=   await  mallUserService.GetUserDetail(token);
            if(rep is null) {
                return Result.FailWithMessage("未查询到记录");
            }
            
            return Result.OkWithData(rep);
        }
        [ServiceFilter(typeof(TokenFilter))]
        [HttpPost("user/logout")]
        public async Task<Result> UserLogout() {
            var token = Request.Headers["Authorization"].ToString()[7..];
        var flag= await   mallUserTokenService.DeleteMallUserToken(token);
            if (!flag) {
                return Result.FailWithMessage("未知错误，登出失败");
            }
            return Result.OkWithMessage("登出成功");
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
