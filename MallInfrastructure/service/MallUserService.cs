using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using IdentityModel;
using K4os.Compression.LZ4.Engine;
using MallDomain.entity.mall;
using MallDomain.entity.mall.request;
using MallDomain.entity.mall.response;
using MallInfrastructure;
using MD5Hash;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace MallDomain.service.mall {
    public class MallUserService : IMallUserService {
        private readonly MallContext mallContext;
        private readonly IConfiguration configuration;
        private readonly JwtSecurityTokenHandler jwtHandler;
        public MallUserService(MallContext mallContext, IConfiguration configuration, JwtSecurityTokenHandler jwtHandler) {
            this.configuration = configuration;
            this.mallContext = mallContext;
            this.jwtHandler = jwtHandler;
        }

        public Task<string> getNewToken(long timeInt, long userId) {
            throw new NotImplementedException();
        }

        public Task<MallUserDetailResponse> GetUserDetail(string token) {
            throw new NotImplementedException();
        }

        // RegisterUser 注册用户
        public async Task RegisterUser(RegisterUserParam req) {

            var mu = await mallContext.MallUsers.Where(r => r.LoginName == req.LoginName).FirstOrDefaultAsync();

            if (mu is null) {


                MallUser user = new();
                user.LoginName = req.LoginName;

                user.PasswordMd5 = req.Password.GetMD5();
                mallContext.MallUsers.Add(user);
                await mallContext.SaveChangesAsync();


            } else {
                throw new Exception("用户名重复");
            }





        }

        public Task UpdateUserInfo(string token, UpdateUserInfoParam req) {
            throw new NotImplementedException();
        }

        public async Task<MallUserToken> UserLogin(UserLoginParam param) {
            var iss = configuration["TokenParameters:iss"];  //发行人
            var aud = configuration["TokenParameters:aud"];  //受众人
            var sign = configuration["TokenParameters:sign"]; //签名凭证


            var us = await mallContext.MallUsers.Where(p => p.LoginName == param.LoginName && p.PasswordMd5 == param.PasswordMd5).SingleAsync();//失败则异常，由异常处理器处理

            //无异常签发token
            var identity = new Claim[] {
                new Claim(JwtClaimTypes.Name, us.LoginName!),
                new Claim(JwtClaimTypes.Id,us.UserId.ToString())
        };

            //生效时间
            var nbf = DateTime.UtcNow;
            //
            var exp = DateTime.UtcNow.AddMinutes(10);
            var secrect = Encoding.UTF8.GetBytes(sign!);

            SymmetricSecurityKey ssk = new(secrect);

            var signingCredentials = new SigningCredentials(ssk, SecurityAlgorithms.HmacSha512);
            /* (string issuer = null, string audience = null, IEnumerable< Claim > claims = null, DateTime? notBefore = null, DateTime? expires = null, SigningCredentials signingCredentials = null)*/

            var jwtoken = new JwtSecurityToken(iss, aud, identity,nbf,exp,signingCredentials);

         var token=  jwtHandler.WriteToken(jwtoken);

            return new MallUserToken() {
                ExpireTime = exp,
                Token = token,
                UserId=us.UserId,
                UpdateTime=nbf
            };

        }
    }
}
