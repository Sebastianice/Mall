
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdentityModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace MallApi.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        public readonly IConfiguration configuration;

        public AuthController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetToken()
        {
            try
            {


                //定义许多种的声明Claim,信息存储部分,Claims的实体一般包含用户和一些元数据
                var claims = new Claim[]
                 {
                new Claim(JwtClaimTypes.Id,"1"),
                new Claim(JwtClaimTypes.Name,"i3yuan"),
                new Claim(JwtClaimTypes.Role,"Admin"),
                };
                //notBefore  生效时间
                var nbf = DateTime.UtcNow;
                //expires   //过期时间
                var Exp = DateTime.UtcNow.AddSeconds(1000);

                //signingCredentials  签名凭证
                var iss = configuration["AdminToken:iss"];  //发行人
                var aud = configuration["AdminToken:aud"];       //受众人
                var sign = configuration["AdminToken:sign"]; //SecurityKey 的长度必须 大于等于 16个字符
                var secret = Encoding.UTF8.GetBytes(sign);
                SymmetricSecurityKey? key = new SymmetricSecurityKey(secret);
                SigningCredentials? signcreds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                JwtSecurityToken? jwt = new JwtSecurityToken(iss, aud, claims, nbf, expires: Exp, signingCredentials: signcreds);
                JwtSecurityTokenHandler? JwtHander = new JwtSecurityTokenHandler();
                string? token = JwtHander.WriteToken(jwt);
                return Ok(new
                {
                    access_token = token,
                    token_type = "Bearer",
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
