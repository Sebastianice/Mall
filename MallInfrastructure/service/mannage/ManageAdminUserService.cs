using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdentityModel;
using MallDomain.entity.mannage;
using MallDomain.entity.mannage.request;
using MallDomain.service.mannage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace MallInfrastructure.service.mannage
{
    public class ManageAdminUserService : IManageAdminUserService
    {
        private readonly MallContext context;
        private readonly IConfiguration configuration;
        // private readonly IMemoryCache cache;
        private readonly JwtSecurityTokenHandler jwtSecurityTokenHandler;

        public ManageAdminUserService
            (MallContext context, IConfiguration configuration,
            JwtSecurityTokenHandler jwtSecurityTokenHandler)

        {
            this.context = context;
            this.configuration = configuration;

            this.jwtSecurityTokenHandler = jwtSecurityTokenHandler;
        }

        public async Task<AdminUserToken> AdminLogin(MallAdminLoginParam param)
        {
            var adminUser = await context.
                               AdminUsers.
                                    SingleOrDefaultAsync(w =>
                                    w.LoginPassword == param.PasswordMd5
                                    &&
                                    w.LoginUserName == param.UserName);

            if (adminUser == null) throw new Exception("用户名密码不正确");

            var token = await generateTokenAsync(adminUser);

            return token;

        }

        public async Task CreateMallAdminUser(AdminUser mallAdminUser)
        {
            var user
                    = context.AdminUsers.
                    FirstOrDefaultAsync(u => u.LoginUserName == mallAdminUser.LoginUserName);

            if (user == null) throw new Exception("用户名已被注册!");

            context.AdminUsers.Add(mallAdminUser);

            await context.SaveChangesAsync();

        }
        public async Task<AdminUserToken> generateTokenAsync(AdminUser user)
        {

            var iss = configuration["AdminToken:iss"];  //发行人
            var aud = configuration["AdminToken:aud"];  //受众人
            var sign = configuration["AdminToken:sign"]; //签名凭证

            var identity = new Claim[] {
                new Claim(JwtClaimTypes.Name, user.LoginUserName!),
                new Claim(JwtClaimTypes.Id,user.AdminUserId.ToString()),
                new Claim(JwtClaimTypes.Role,"Admin")
        };

            //生效时间
            var nbf = DateTime.UtcNow;
            //
            var exp = DateTime.UtcNow.AddMinutes(10);
            var secrect = Encoding.UTF8.GetBytes(sign!);

            SymmetricSecurityKey ssk = new(secrect);

            var signingCredentials = new SigningCredentials(ssk, SecurityAlgorithms.Aes128CbcHmacSha256);

            var jwtoken = new JwtSecurityToken(iss, aud, identity, nbf, exp, signingCredentials);

            var token = "Bearer " + jwtSecurityTokenHandler.WriteToken(jwtoken);


            //查询是否存在token
            //没有就新建，存在就覆盖，签发新token    
            var oldtoken = await context.
                                 AdminUserTokens.
                                 SingleOrDefaultAsync(s =>
                                                      s.AdminUserId
                                                       ==
                                                      user.AdminUserId);


            if (oldtoken == null)
            {
                oldtoken = new AdminUserToken
                {
                    ExpireTime = exp,
                    Token = token,
                    UpdateTime = nbf,

                };
                await context.AddAsync(oldtoken);
            }
            else
            {
                oldtoken.ExpireTime = exp.ToUniversalTime();
                oldtoken.Token = token;
                oldtoken.UpdateTime = nbf.ToUniversalTime();
            }


            await context.SaveChangesAsync();

            //  cache.Set("Admin" + oldtoken.AdminUserId, token);

            return oldtoken!;
        }

        public async Task<AdminUser> GetMallAdminUser(string token)
        {
            var adminToken = await context.AdminUserTokens
                                 .SingleOrDefaultAsync(w => w.Token == token);

            if (adminToken is null) throw new Exception("不存在该账户!");

            var adminUser = await context.AdminUsers.SingleOrDefaultAsync
                                (w => w.AdminUserId == adminToken.AdminUserId);

            if (adminUser is null) throw new Exception("未查到该用户信息");

            return adminUser;
        }

        public async Task UpdateMallAdminName(string token, MallUpdateNameParam param)
        {
            var adminToken = await context.AdminUserTokens
                                .SingleOrDefaultAsync(w => w.Token == token);

            if (adminToken is null) throw new Exception("不存在该账户!");

            var adminUser = await context.AdminUsers.SingleOrDefaultAsync
                                (w => w.AdminUserId == adminToken.AdminUserId);

            if (adminUser is null) throw new Exception("未查到该用户信息");

            adminUser.LoginUserName = param.LoginUserName;
            adminUser.NickName = param.NickName;

            await context.SaveChangesAsync();


        }

        public async Task UpdateMallAdminPassWord(string token, MallUpdatePasswordParam param)
        {
            var adminToken = await context.AdminUserTokens
                                .SingleOrDefaultAsync(w => w.Token == token);

            if (adminToken is null) throw new Exception("不存在该账户!");

            var adminUser = await context.AdminUsers.SingleOrDefaultAsync
                                (w => w.AdminUserId == adminToken.AdminUserId);

            if (adminUser is null) throw new Exception("未查到该用户信息");

            if (adminUser.LoginPassword != param.OriginalPassword)
            {
                throw new Exception("原密码不正确");
            }

            adminUser.LoginPassword = param.NewPassword;

            await context.SaveChangesAsync();

        }
    }
}
