using MallDomain.entity.common.response;
using MallDomain.entity.mannage;
using MallDomain.entity.mannage.request;
using MallDomain.service.manage;
using MallDomain.service.mannage;
using MallDomain.utils.validator;
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
        private readonly IManageUserService manageUserService;
        public ManageAdminUserController(IManageAdminUserService manageAdminUserService, IManageAdminTokenService mnanageAdminTokenService, IManageUserService manageUserService)
        {
            this.manageAdminUserService = manageAdminUserService;
            this.mananageAdminTokenService = mnanageAdminTokenService;
            this.manageUserService = manageUserService;
        }

        [HttpPost("createMallAdminUser")]
        public async Task<Result> CreateAdminUser([FromBody] AdminParam mallAdminParam)
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
                  CreateMallAdminUser(mallAdminParam.Adapt<AdminUser>());


            return Result.Ok();
        }


        [HttpPut("adminUser/name")]
        public async Task<Result> UpdateAdminUserName([FromBody] UpdateNameParam req)
        {

            var token = Request.Headers["Authorization"];
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
            var token = Request.Headers["Authorization"];
            var v = await ValidatorFactory.CreateValidator(req)!.ValidateAsync(req!);

            if (!v.IsValid)
            {
                string msg = string.Join(";", v.Errors.Select(w => w.ErrorMessage));
                return Result.FailWithMessage(msg);
            }
            await manageAdminUserService.UpdateMallAdminPassWord(token, req);

            return Result.OkWithMessage("更新用户名昵称成功");
        }

        // UserList 商城注册用户列表
        [HttpGet("users")]
        public async Task<Result> UserList([FromQuery] UserSearch search)
        {
            var (list, total) = await manageUserService.GetMallUserInfoList(search);

            return Result.OkWithDetailed(new PageResult()
            {
                List = list,
                TotalCount = total,
                CurrPage = search.PageInfo.PageNumber,
                PageSize = search.PageInfo.PageSize
            }, "获取成功");
        }


        [HttpPut("users/{lockStatus}")]
        public async Task<Result> LockUser([FromBody] List<long> Ids)
        {
            await manageUserService.LockUser(Ids);

            return Result.OkWithMessage("更新成功");
        }


        [HttpGet("adminUser/profile")]
        public async Task<Result> AdminUserProfile()
        {
            var token = Request.Headers["Authorization"];
            var admin = await manageAdminUserService.GetMallAdminUser(token!);

            return Result.OkWithData(admin);
        }

        [HttpDelete("logout")]
        public async Task<Result> AdminLogout()
        {
            var token = Request.Headers["Authorization"];
            await mananageAdminTokenService.DeleteMallAdminUserToken(token!);
            return Result.Ok();
        }


        [HttpPost("upload/file")]
        public async Task<Result> UploadFile(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var filePath = Path.GetTempFileName();
                    

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
                await Console.Out.WriteLineAsync();
            }


            return Result.Ok();
        }


        [HttpPost("adminUser/login")]
        public async Task<Result> AdminLogin([FromBody] AdminLoginParam req)
        {
            var v = await ValidatorFactory.CreateValidator(req)!.ValidateAsync(req!);

            if (!v.IsValid)
            {
                string msg = string.Join(";", v.Errors.Select(w => w.ErrorMessage));
                return Result.FailWithMessage(msg);
            }

            var adminToken = await manageAdminUserService.AdminLogin(req);

            return Result.OkWithDetailed(adminToken.Token, "管理员登录成功");
        }
    }
}
