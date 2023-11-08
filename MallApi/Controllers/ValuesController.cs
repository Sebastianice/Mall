using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MallApi.Controllers
{
    [Route("api/v")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public readonly IConfiguration configuration;

        public ValuesController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // GET: api/<ValuesController>
        [HttpGet("UserGet")]
        [Authorize(policy: "User")]
        public string UserGet()
        {
            var token = Request.Headers["Authorization"].ToString();
           
            return "User:" + token;
        }

        // GET api/<ValuesController>/5
        [HttpGet("AdminGet")]
        [Authorize(policy: "Admin")]
        public string AdminGet()
        {
            var authorization = Request.Headers["Authorization"].ToString();
        
            return "Admin:" + authorization;
        }

        [HttpGet("SignAdminToken")]
        public IActionResult SignAdminToken()
        {
            try
            {


                //定义许多种的声明Claim,信息存储部分,Claims的实体一般包含用户和一些元数据
                var claims = new Claim[]
                 {
                new Claim(JwtClaimTypes.Id,"2"),
                new Claim(JwtClaimTypes.Name,"管理员"),
                new Claim(JwtClaimTypes.Role,"Admin"),
                };
                //notBefore  生效时间
                var nbf = DateTime.UtcNow;
                //expires   //过期时间
                var Exp = DateTime.UtcNow.AddSeconds(60);

                //signingCredentials  签名凭证
                var iss = configuration["AdminToken:iss"];  //发行人
                var aud = configuration["AdminToken:aud"];       //受众人
                var sign = configuration["AdminToken:sign"]; //SecurityKey 的长度必须 大于等于 16个字符
                var secret = Encoding.UTF8.GetBytes(sign);
                SymmetricSecurityKey? key = new SymmetricSecurityKey(secret);
                SigningCredentials? signcreds = new SigningCredentials(key, SecurityAlgorithms.Aes128CbcHmacSha256);
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
        [HttpGet("SigntUserToken")]
        public IActionResult SigntUserToken()
        {
            try
            {


                //定义许多种的声明Claim,信息存储部分,Claims的实体一般包含用户和一些元数据
                var claims = new Claim[]
                 {
                new Claim(JwtClaimTypes.Id,"8"),
                new Claim(JwtClaimTypes.Name,"用户"),
                new Claim(JwtClaimTypes.Role,"User"),
                };
                //notBefore  生效时间
                var nbf = DateTime.UtcNow;
                //expires   //过期时间
                var Exp = DateTime.UtcNow.AddSeconds(60);

                //signingCredentials  签名凭证
                var iss = configuration["AdminToken:iss"];  //发行人
                var aud = configuration["AdminToken:aud"];       //受众人
                var sign = configuration["AdminToken:sign"]; //SecurityKey 的长度必须 大于等于 16个字符
                var secret = Encoding.UTF8.GetBytes(sign);
                SymmetricSecurityKey? key = new SymmetricSecurityKey(secret);
                SigningCredentials? signcreds = new SigningCredentials(key, SecurityAlgorithms.Aes128CbcHmacSha256);
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
                throw ex;
            }

        }


    }
}
