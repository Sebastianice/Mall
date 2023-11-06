using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdentityModel;
using MallDomain.entity.mall;
using MallDomain.entity.mall.request;
using MallDomain.entity.mall.response;
using MallInfrastructure;
using Mapster;
using MD5Hash;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace MallDomain.service.mall
{
    public class MallUserService : IMallUserService
    {
        private readonly MallContext context;
        private readonly IConfiguration configuration;
        private readonly JwtSecurityTokenHandler jwtHandler;
        private readonly IMemoryCache cache;

        public MallUserService(MallContext mallContext, IConfiguration configuration, JwtSecurityTokenHandler jwtHandler, IMemoryCache icache)
        {
            this.configuration = configuration;
            this.context = mallContext;
            this.jwtHandler = jwtHandler;
            this.cache = icache;
        }

        public Task<string> getNewToken(long timeInt, long userId)
        {
            throw new NotImplementedException();
        }

        public async Task<MallUserDetailResponse?> GetUserDetail(string token)
        {
            var userToken = await context.MallUserTokens.SingleOrDefaultAsync(f => f.Token == token);

            if (userToken == null)
            {
                return null;
            }
            var user = await context.MallUsers.SingleOrDefaultAsync(f => f.UserId == userToken.UserId);
            if (user is null)
            {
                return null;
            }
            return user.Adapt<MallUserDetailResponse>();
        }

        // RegisterUser 注册用户
        public async Task RegisterUser(RegisterUserParam req)
        {

            var mu = await context.MallUsers.Where(r => r.LoginName == req.LoginName).FirstOrDefaultAsync();

            if (mu is null)
            {


                MallUser user = new();
                user.LoginName = req.LoginName;

                user.PasswordMd5 = req.Password.GetMD5();
                context.MallUsers.Add(user);
                await context.SaveChangesAsync();


            }
            else
            {
                throw new Exception("用户名重复");
            }





        }

        public async Task<bool> UpdateUserInfo(string token, UpdateUserInfoParam req)
        {
            var userToken = await context.MallUserTokens.SingleOrDefaultAsync(f => f.Token == token);
            if (userToken == null) return false;
            var user = await context.MallUsers.SingleOrDefaultAsync(f => f.UserId == userToken.UserId);

            if (user == null) return false;
            user.PasswordMd5 = req.PasswordMd5;
            user.NickName = req.NickName;
            user.IntroduceSign = req.IntroduceSign;

            await context.SaveChangesAsync();

            return true;
        }

        public async Task<MallUserToken> UserLogin(UserLoginParam param)
        {
            var iss = configuration["UserToken:iss"];  //发行人
            var aud = configuration["UserToken:aud"];  //受众人
            var sign = configuration["UserToken:sign"]; //签名凭证


            var us = await context.MallUsers.Where(p => p.LoginName == param.LoginName && p.PasswordMd5 == param.PasswordMd5).SingleAsync();//失败则异常，由异常处理器处理

            //查询是否存在token


            //没有就新建，存在就覆盖，签发新token               
            //签发token
            var identity = new Claim[] {
                new Claim(JwtClaimTypes.Name, us.LoginName!),
                new Claim(JwtClaimTypes.Id,us.UserId.ToString()),
                new Claim(JwtClaimTypes.Role,"User")
        };

            //生效时间
            var nbf = DateTime.UtcNow;
            //
            var exp = DateTime.UtcNow.AddMinutes(10);
            var secrect = Encoding.UTF8.GetBytes(sign!);

            SymmetricSecurityKey ssk = new(secrect);

            var signingCredentials = new SigningCredentials(ssk, SecurityAlgorithms.Aes128CbcHmacSha256);

            var jwtoken = new JwtSecurityToken(iss, aud, identity, nbf, exp, signingCredentials);

            var token = jwtHandler.WriteToken(jwtoken);

            var oldtoken = await context.MallUserTokens.Where(s => s.UserId == us.UserId).SingleOrDefaultAsync();
            if (oldtoken == null)
            {
                oldtoken = new MallUserToken
                {
                    ExpireTime = exp,
                    Token = token,
                    UpdateTime = nbf,

                };
                await context.AddAsync(oldtoken);
            }
            else
            {
                oldtoken.ExpireTime = exp;
                oldtoken.Token = token;
                oldtoken.UpdateTime = nbf;
            }

            await context.SaveChangesAsync();

            cache.Set("user" + oldtoken.UserId, token);

            return oldtoken;












        }
    }
}
