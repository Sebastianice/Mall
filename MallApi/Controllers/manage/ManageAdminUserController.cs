using MallDomain.entity.common.response;
using MallDomain.entity.mannage;
using MallDomain.entity.mannage.request;
using MallDomain.service.mannage;
using MallDomain.utils;
using MallInfrastructure.service.mall;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MallApi.Controllers.mannage
{

    [Route("manage-api/v1")]
    [ApiController]
    [Authorize(policy: "Admin")]
    public class ManageAdminUserController : ControllerBase
    {
        private readonly IManageAdminUserService manageAdminUserService;
        private readonly IManageAdminTokenService mananageAdminTokenService;

        public ManageAdminUserController(IManageAdminUserService manageAdminUserService, IManageAdminTokenService mnanageAdminTokenService)
        {
            this.manageAdminUserService = manageAdminUserService;
            this.mananageAdminTokenService = mnanageAdminTokenService;
        }

        [HttpPost("createMallAdminUser")]
        public async Task<Result> CreateAdminUser([FromBody] MallAdminParam mallAdminParam)
        {

            var validator = ValidatorFactory.CreateValidator(mallAdminParam);

            var vResult = validator!.Validate(mallAdminParam);

            if (!vResult.IsValid)
            {
                string msg = string.
                    Join(";", vResult.
                                       Errors.
                                       Select(e => e.ErrorMessage));
                return Result.FailWithMessage(msg);   
            }


           await manageAdminUserService.
                 CreateMallAdminUser(mallAdminParam.Adapt<MallAdminUser>());


            return Result.Ok();
        }


        [HttpPut("adminUser/name")]
        public async Task<Result> UpdateAdminUserName([FromBody] MallUpdateNameParam req)
        {

            var token = Request.Headers["Authorization"].ToString()[7..];
            var v = await ValidatorFactory.CreateValidator(req)!.ValidateAsync(req!);

            if (!v.IsValid)
            {
                string msg = string.Join(";", v.Errors.Select(w => w.ErrorMessage));
                return Result.FailWithMessage(msg);
            }
            await manageAdminUserService.UpdateMallAdminName(token, req);
            
            return Result.OkWithMessage("更新用户名昵称成功");
        }


        [HttpPut("adminUser/password")]
        public async Task<Result> UpdateAdminUserPassword([FromBody] MallUpdatePasswordParam req)
        {
            var token = Request.Headers["Authorization"].ToString()[7..];
            var v = await ValidatorFactory.CreateValidator(req)!.ValidateAsync(req!);

            if (!v.IsValid)
            {
                string msg = string.Join(";", v.Errors.Select(w => w.ErrorMessage));
                return Result.FailWithMessage(msg);
            }
            await manageAdminUserService.UpdateMallAdminPassWord(token, req);

            return Result.OkWithMessage("更新用户名昵称成功");
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
            var token = Request.Headers["Authorization"].ToString()[7..];
           await mananageAdminTokenService.DeleteMallAdminUserToken(token);
            return Result.Ok();
        }


       /* [HttpPost("upload/file")]  不打算实现了
        public async Task<Result> UploadFile()
        {
            return Result.Ok();
        }*/


        [HttpPost("adminUser/login")]
        public async Task<Result> AdminLogin([FromBody]MallAdminLoginParam req)
        {
          var v=  await ValidatorFactory.CreateValidator(req)!.ValidateAsync(req!);

            if (!v.IsValid)
            {
                string msg = string.Join(";", v.Errors.Select(w => w.ErrorMessage));
                return Result.FailWithMessage(msg);
            }
            
            var adminToken = await manageAdminUserService.AdminLogin(req);
            
            return Result.OkWithDetailed(adminToken,"管理员登录成功");
        }
    }
}
