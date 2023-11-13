using Mall.Common.Result;
using Mall.Repository.Models;
using Mall.Services;
using Mall.Services.Models;
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
        private readonly ManageAdminUserService manageAdminUserService;
        private readonly ManageAdminTokenService mananageAdminTokenService;
        private readonly ManageUserService manageUserService;
        private readonly IWebHostEnvironment _environment;


        public ManageAdminUserController(ManageAdminUserService manageAdminUserService, ManageAdminTokenService mnanageAdminTokenService, ManageUserService manageUserService, IWebHostEnvironment environment)
        {
            this.manageAdminUserService = manageAdminUserService;
            this.mananageAdminTokenService = mnanageAdminTokenService;
            this.manageUserService = manageUserService;
            _environment = environment;
        }
        [AllowAnonymous]
        [HttpPost("createMallAdminUser")]
        public async Task<AppResult> CreateAdminUser([FromBody] AdminParam mallAdminParam)
        {

            /*  var validator = ValidatorFactory.CreateValidator(mallAdminParam);

              var vResult = validator!.Validate(mallAdminParam);

              if (!vResult.IsValid)
              {
                  string msg = string.
                      Join(";", vResult.
                                         Errors.
                                         Select(e => e.ErrorMessage));
                  return AppResult.FailWithMessage(msg);
              }*/


            await manageAdminUserService.
                  CreateMallAdminUser(mallAdminParam.Adapt<AdminUser>());


            return AppResult.Ok();
        }


        [HttpPut("adminUser/name")]
        public async Task<AppResult> UpdateAdminUserName([FromBody] UpdateNameParam req)
        {

            var token = Request.Headers["Authorization"]!;
          /*  var v = await ValidatorFactory.CreateValidator(req)!.ValidateAsync(req!);

            if (!v.IsValid)
            {
                string msg = string.Join(";", v.Errors.Select(w => w.ErrorMessage));
                return AppResult.FailWithMessage(msg);
            }*/
            await manageAdminUserService.UpdateMallAdminName(token!, req);

            return AppResult.OkWithMessage("更新用户名昵称成功");
        }


        [HttpPut("adminUser/password")]
        public async Task<AppResult> UpdateAdminUserPassword([FromBody] MallUpdatePasswordParam req)
        {
            var token = Request.Headers["Authorization"]!;
         /*   var v = await ValidatorFactory.CreateValidator(req)!.ValidateAsync(req!);

            if (!v.IsValid)
            {
                string msg = string.Join(";", v.Errors.Select(w => w.ErrorMessage));
                return AppResult.FailWithMessage(msg);
            }*/
            await manageAdminUserService.UpdateMallAdminPassWord(token!, req);

            return AppResult.OkWithMessage("更新用户名昵称成功");
        }

        // UserList 商城注册用户列表
        [HttpGet("users")]
        public async Task<AppResult> UserList([FromQuery] PageInfo search)
        {
            var (list, total) = await manageUserService.GetMallUserInfoList(search);

            return AppResult.OkWithDetailed(new PageResult()
            {
                List = list,
                TotalCount = total,
                CurrPage = search.PageNumber,
                PageSize = search.PageSize,
                TotalPage = (int)Math.Ceiling((double)total / search.PageSize)
            }, "获取成功");
        }


        [HttpPut("users/{lockStatus}")]
        public async Task<AppResult> LockUser([FromBody] IdsReq ids, sbyte lockStatus)
        {
            await manageUserService.LockUser(ids.Ids, lockStatus);

            return AppResult.OkWithMessage("更新成功");
        }


        [HttpGet("adminUser/profile")]
        public async Task<AppResult> AdminUserProfile()
        {
            var token = Request.Headers["Authorization"]!;
            var admin = await manageAdminUserService.GetMallAdminUser(token!);

            return AppResult.OkWithData(admin);
        }

        [HttpDelete("logout")]
        public async Task<AppResult> AdminLogout()
        {
            var token = Request.Headers["Authorization"]!;
            await mananageAdminTokenService.DeleteMallAdminUserToken(token!);
            return AppResult.Ok();
        }


        [HttpPost("upload/file")]
        public async Task<AppResult> UploadFile(IFormFile file)
        {
            //long size = file.Sum(f => f.Length);



            if (file.Length > 0)
            {


                // 生成随机的文件名
                string fileName = Path.GetRandomFileName() + Path.GetExtension(file.FileName);
                // 获取静态文件夹的物理路径
                string uploadsFolder = Path.Combine(_environment.ContentRootPath, "staticfiles");

                // 拼接文件的完整路径
                string filePath = Path.Combine(uploadsFolder, fileName);

                // 将文件保存到静态文件夹
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                return AppResult.OkWithData("http://localhost:5047/staticfiles/" + fileName);
            }



            return AppResult.FailWithMessage("接收文件失败");



        }

        [AllowAnonymous]
        [HttpPost("adminUser/login")]
        public async Task<AppResult> AdminLogin([FromBody] AdminLoginParam req)
        {
          /*  var v = await ValidatorFactory.CreateValidator(req)!.ValidateAsync(req!);

            if (!v.IsValid)
            {
                string msg = string.Join(";", v.Errors.Select(w => w.ErrorMessage));
                return AppResult.FailWithMessage(msg);
            }
*/
            var adminToken = await manageAdminUserService.AdminLogin(req);

            return AppResult.OkWithDetailed(adminToken.Token, "管理员登录成功");
        }
    }
}
