using Mall.Common.Authentication.Jwt;
using Mall.Common.Components.Authenrization.Jwt;
using Mall.Common.Result;
using Mall.Repository;
using Mall.Repository.Models;
using Mall.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace Mall.Services
{
    public class ManageAdminUserService
    {
        private readonly MallContext context;

        // private readonly IMemoryCache cache;


        public ManageAdminUserService (MallContext context )
        {
            this.context = context;
        }

        public async Task<AdminUserToken> AdminLogin(AdminLoginParam param)
        {
            var adminUser = await context.
                               AdminUsers.
                                    SingleOrDefaultAsync(w =>
                                    w.LoginPassword == param.PasswordMd5
                                    &&
                                    w.LoginUserName == param.UserName);

            if (adminUser == null) throw ResultException.FailWithMessage("用户名密码不正确");
            //签发token
            var jwtModel = new JwtTokenModel();
            jwtModel.UserId = adminUser.AdminUserId.ToString();
            jwtModel.UserName = adminUser.LoginUserName;
            jwtModel.UserRole = "Admin";

            var token = JwtHelper.Create(jwtModel);

            //查询是否存在token
            //没有就新建，存在就覆盖，签发新token    
            var oldtoken = await context.AdminUserTokens.
                Where(s => s.AdminUserId == adminUser.AdminUserId).
                SingleOrDefaultAsync();


            if (oldtoken == null)
            {
                oldtoken = new AdminUserToken
                {
                    AdminUserId = adminUser.AdminUserId,

                    Token = token,
                    UpdateTime = DateTime.UtcNow,

                };
                await context.AddAsync(oldtoken);
            }
            else
            {

                oldtoken.Token = token;
                oldtoken.UpdateTime = DateTime.UtcNow;
            }


            await context.SaveChangesAsync();


            return oldtoken;




        }

        public async Task CreateMallAdminUser(AdminUser mallAdminUser)
        {
            var user
                    = context.AdminUsers.
                    FirstOrDefaultAsync(u => u.LoginUserName == mallAdminUser.LoginUserName);

            if (user == null) throw ResultException.FailWithMessage("用户名已被注册!");

            context.AdminUsers.Add(mallAdminUser);

            await context.SaveChangesAsync();

        }


        public async Task<AdminUser> GetMallAdminUser(string token)
        {
            var JWT = await context.AdminUserTokens
                                 .SingleOrDefaultAsync(w => w.Token == token);

            if (JWT is null) throw ResultException.FailWithMessage("不存在该账户!");

            var adminUser = await context.AdminUsers.SingleOrDefaultAsync
                                (w => w.AdminUserId == JWT.AdminUserId);

            if (adminUser is null) throw ResultException.FailWithMessage("未查到该用户信息");

            return adminUser;
        }

        public async Task UpdateMallAdminName(string token, UpdateNameParam param)
        {
            var JWT = await context.AdminUserTokens
                                .SingleOrDefaultAsync(w => w.Token == token);

            if (JWT is null) throw ResultException.FailWithMessage("不存在该账户!");

            var adminUser = await context.AdminUsers.SingleOrDefaultAsync
                                (w => w.AdminUserId == JWT.AdminUserId);

            if (adminUser is null) throw ResultException.FailWithMessage("未查到该用户信息");

            adminUser.LoginUserName = param.LoginUserName ?? adminUser.LoginUserName;
            adminUser.NickName = param.NickName ?? adminUser.NickName;

            await context.SaveChangesAsync();


        }

        public async Task UpdateMallAdminPassWord(string token, MallUpdatePasswordParam param)
        {
            var JWT = await context.AdminUserTokens
                                .SingleOrDefaultAsync(w => w.Token == token);

            if (JWT is null) throw ResultException.FailWithMessage("不存在该账户!");

            var adminUser = await context.AdminUsers.SingleOrDefaultAsync
                                (w => w.AdminUserId == JWT.AdminUserId);

            if (adminUser is null) throw ResultException.FailWithMessage("未查到该用户信息");

            if (adminUser.LoginPassword != param.OriginalPassword)
            {
                throw ResultException.FailWithMessage("原密码不正确");
            }

            adminUser.LoginPassword = param.NewPassword!;

            await context.SaveChangesAsync();

        }
    }
}
