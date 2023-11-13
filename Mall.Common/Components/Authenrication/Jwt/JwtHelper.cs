using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdentityModel;
using Mall.Common.Components.Authenrization.Jwt;
using Mall.Common.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Mall.Common.Authentication.Jwt;

public static class JwtHelper
{
    /// <summary>
    /// 生成 JWT Token
    /// </summary>
    /// <param name="tokenModel"></param>
    /// <returns></returns>
    public static string Create(JwtTokenModel tokenModel)
    {
        // 获取配置
        string issuer = AppSettings.Jwt.Issuer;
        string audience = AppSettings.Jwt.Audience;
        string secret = AppSettings.Jwt.SecretKey;

        var identity = new Claim[] {
                new Claim(JwtClaimTypes.Name, tokenModel.UserName),
                new Claim(JwtClaimTypes.Id,tokenModel.UserId),
                new Claim(JwtClaimTypes.Role,tokenModel.UserRole)
        };


        //生效时间
        var nbf = DateTime.UtcNow;
        //
        var exp = tokenModel.Exp ?? DateTime.UtcNow.AddMinutes(30);
        var secrect = Encoding.UTF8.GetBytes(secret);

        SymmetricSecurityKey ssk = new(secrect);

        var signingCredentials = new SigningCredentials(ssk, SecurityAlgorithms.HmacSha256);

        var jwtoken = new JwtSecurityToken(issuer, audience, identity, nbf, exp, signingCredentials);

        var jwthandler = new JwtSecurityTokenHandler();


        var token = "Bearer " + jwthandler.WriteToken(jwtoken);


        return token;
    }
}
