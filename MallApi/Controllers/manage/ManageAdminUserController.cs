using MallDomain.entity.common.request;
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
        private readonly IWebHostEnvironment _environment;

       
        public ManageAdminUserController(IManageAdminUserService manageAdminUserService, IManageAdminTokenService mnanageAdminTokenService, IManageUserService manageUserService, IWebHostEnvironment environment)
        {
            this.manageAdminUserService = manageAdminUserService;
            this.mananageAdminTokenService = mnanageAdminTokenService;
            this.manageUserService = manageUserService;
            _environment = environment;
        }
        [AllowAnonymous]
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
        public async Task<Result> UserList([FromQuery] PageInfo search)
        {
            var (list, total) = await manageUserService.GetMallUserInfoList(search);

            return Result.OkWithDetailed(new PageResult()
            {
                List = list,
                TotalCount = total,
                CurrPage = search.PageNumber,
                PageSize = search.PageSize,
                TotalPage = (int)Math.Ceiling((double)total / search.PageSize)
            }, "获取成功");
        }


        [HttpPut("users/{lockStatus}")]
        public async Task<Result> LockUser([FromBody] IdsReq ids,sbyte lockStatus)
        {
            await manageUserService.LockUser(ids.Ids, lockStatus);

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
        public async Task<Result> UploadFile([FromForm]List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                   

                    // 生成随机的文件名
                    string fileName = Path.GetRandomFileName() + Path.GetExtension(formFile.FileName);
                    // 获取静态文件夹的物理路径
                    string uploadsFolder = Path.Combine(_environment.ContentRootPath, "staticfiles");

                    // 拼接文件的完整路径
                    string filePath = Path.Combine(uploadsFolder, fileName);

                    // 将文件保存到静态文件夹
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(fileStream);
                    }
                    return Result.OkWithData("http://localhost:5047/staticfiles/"+fileName);
                }
                     
            }

            return Result.FailWithMessage("接收文件失败");



        }

        [AllowAnonymous]
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
