using Mall.Common.Result;
using Microsoft.Extensions.Configuration;

namespace Mall.Common.Configuration;

public static class AppSettings
{
    public static IConfiguration? _configuration;
    public static IConfiguration Configuration
    {
        get
        {
            return _configuration = _configuration ?? throw new ArgumentNullException(nameof(Configuration));
        }
    }
    /// <summary>
    /// 允许跨域请求列表
    /// </summary>
    public static string[] AllowCors => Configuration.GetSection("AllowCors").Get<string[]>();

    /// <summary>
    /// Jwt 配置
    /// </summary>
    public static class Jwt
    {
        public static string SecretKey => Configuration["Jwt:sign"];
        public static string Issuer => Configuration["Jwt:iss"];
        public static string Audience => Configuration["Jwt:aud"];
    }

    public static void Configure(IConfiguration configuration)
    {
        if (_configuration != null)
        {
            throw ResultException.FailWithMessage($"{nameof(Configuration)}不可修改！");
        }
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }
}



