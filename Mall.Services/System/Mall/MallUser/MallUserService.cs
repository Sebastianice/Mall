
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdentityModel;
using Mall.Common.Authentication.Jwt;
using Mall.Common.Components.Authenrization.Jwt;
using Mall.Common.Result;
using Mall.Repository;
using Mall.Repository.Models;
using Mall.Services.Models;
using Mapster;
using MD5Hash;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Mall.Services
{
    public class MallUserService
    {
        private readonly MallContext context;
        private readonly IConfiguration configuration;
      
        //private readonly IMemoryCache cache;

        public MallUserService(MallContext mallContext, IConfiguration configuration)
        {
            this.configuration = configuration;
            context = mallContext;
         

        }

    

        public async Task<UserDetailResponse> GetUserDetail(string token)
        {
            var userToken = await context.UserTokens.SingleOrDefaultAsync(f => f.Token == token);

            if (userToken == null) ResultException.FailWithMessage("该用户不存在");

            var user = await context.Users.SingleOrDefaultAsync(f => f.UserId == userToken!.UserId);

            if (user is null) ResultException.FailWithMessage("该用户不存在或被冻结");

            return user.Adapt<UserDetailResponse>();
        }

        // RegisterUser 注册用户
        public async Task RegisterUser(RegisterUserParam req)
        {

            var mu = await context.Users.
                Where(r => r.LoginName == req.LoginName).
                FirstOrDefaultAsync();

            if (mu is null)
            {
                User user = new();
                user.LoginName = req.LoginName ?? user.LoginName;

                user.PasswordMd5 = req.Password.GetMD5();
                context.Users.Add(user);
                await context.SaveChangesAsync();

            }
            else
            {
                throw ResultException.FailWithMessage("用户名重复");
            }

        }

        public async Task UpdateUserInfo(string token, UpdateUserInfoParam req)
        {
            var userToken = await context.UserTokens.
                SingleOrDefaultAsync(f => f.Token == token);

            if (userToken == null) throw ResultException.FailWithMessage("该用户不存在");

            var user = await context.Users.
                SingleOrDefaultAsync(f => f.UserId == userToken.UserId);

            if (user == null) throw ResultException.FailWithMessage("该用户不存在");

            user.PasswordMd5 = req.PasswordMd5 ?? user.PasswordMd5;
            user.NickName = req.NickName ?? user.NickName;
            user.IntroduceSign = req.IntroduceSign ?? user.IntroduceSign;

            await context.SaveChangesAsync();


        }

        public async Task<UserToken> UserLogin(UserLoginParam param)
        {

            var us = await context.Users.
                Where(p => p.LoginName == param.LoginName && p.PasswordMd5 == param.PasswordMd5).SingleOrDefaultAsync();

            //失败则异常，由异常处理器处理
            if (us is null) throw ResultException.FailWithMessage("用户名或密码不对");



            //签发token
            var jwtModel = new JwtTokenModel();
            jwtModel.UserId=us.UserId.ToString();
            jwtModel.UserName = us.LoginName;
            jwtModel.UserRole = "User";

            var token = JwtHelper.Create(jwtModel);
          
            //查询是否存在token
            //没有就新建，存在就覆盖，签发新token    
            var oldtoken = await context.UserTokens.
                Where(s => s.UserId == us.UserId).
                SingleOrDefaultAsync();


            if (oldtoken == null)
            {
                oldtoken = new UserToken
                {
                    UserId = us.UserId,
               
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

      


    }
}
