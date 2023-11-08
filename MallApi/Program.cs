using System.IdentityModel.Tokens.Jwt;
using System.Text;
using EntityFramework.Exceptions.MySQL;
using MallApi.filter;
using MallApi.middleware;
using MallDomain.service.mall;
using MallDomain.service.mannage;
using MallInfrastructure;
using MallInfrastructure.service.mall;
using MallInfrastructure.service.mannage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;


#region 服务注册
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(opts =>
{
    opts.Filters.Add(typeof(GExceptionFilter));

});


builder.Services.AddLogging(builder =>
{
    builder.ClearProviders();
    builder.SetMinimumLevel(LogLevel.Debug);

});

builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(g =>
{
    var scheme = new OpenApiSecurityScheme()
    {
        Description = "Authorization header. \r\nExample: 'Bearer 12345abcdef'",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Authorization"
        },
        Scheme = "oauth2",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
    };
    g.AddSecurityDefinition("Authorization", scheme);
    var requirement = new OpenApiSecurityRequirement();
    requirement[scheme] = new List<string>();
    g.AddSecurityRequirement(requirement);
});

builder.Services.AddScoped<IAuthorizationHandler, MyAuthorizationHandler>();
builder.Services.AddScoped<IMallGoodsCategoryService, MallGoodsCategoryService>();
builder.Services.AddScoped<IMallGoodsInfoService, MallGoodsInfoService>();
builder.Services.AddScoped<IMallCarouselService, MallCarouselService>();
builder.Services.AddScoped<IMallIndexInfoService, MallIndexInfoService>();
builder.Services.AddScoped<IMallOrderService, MallOrderService>();
builder.Services.AddScoped<IMallUserService, MallUserService>();
builder.Services.AddScoped<JwtSecurityTokenHandler>();
builder.Services.AddScoped<IMallShopCartService, MallShopCartService>();
builder.Services.AddScoped<IMallUserAddressService, MallUserAddressService>();
builder.Services.AddScoped<IMallUserTokenService, MallUserTokenService>();
builder.Services.AddScoped<IManageAdminUserService, ManageAdminUserService>();
builder.Services.AddScoped<IManageAdminTokenService, ManageAdminTokenService>();



//builder.Services.AddMemoryCache();
builder.Services.AddCors(o =>
{
    o.AddPolicy("local", p =>
    {
        p.WithOrigins(new string[] { "http://localhost:8080" })
       .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
       

    });
});

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer("AdminScheme", o =>
{
    var Issurer = builder.Configuration["AdminToken:iss"];  //发行人
    var Audience = builder.Configuration["AdminToken:aud"];       //受众人
    var secretCredentials = builder.Configuration["AdminToken:sign"];
    o.TokenValidationParameters = new TokenValidationParameters
    {
        //是否验证发行人
        ValidateIssuer = true,
        ValidIssuer = Issurer,//发行人
                              //是否验证受众人
        ValidateAudience = true,
        ValidAudience = Audience,//受众人
                                 //是否验证密钥
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretCredentials)),

        ValidateLifetime = true, //验证生命周期
        RequireExpirationTime = true, //过期时间
    };

}).AddJwtBearer("UserScheme", o =>
{
    var Issurer = builder.Configuration["UserToken:iss"];  //发行人
    var Audience = builder.Configuration["UserToken:aud"];       //受众人
    var secretCredentials = builder.Configuration["UserToken:sign"];
    o.TokenValidationParameters = new TokenValidationParameters
    {
        //是否验证发行人
        ValidateIssuer = true,
        ValidIssuer = Issurer,//发行人
                              //是否验证受众人
        ValidateAudience = true,
        ValidAudience = Audience,//受众人
                                 //是否验证密钥
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretCredentials)),

        ValidateLifetime = true, //验证生命周期
        RequireExpirationTime = true, //过期时间
    };

});

builder.Services.AddAuthorization(builder =>
{
    builder.AddPolicy("User", p =>
    {
        p.AddAuthenticationSchemes("UserScheme");
        p.AddRequirements(new MyAuthorizationRequirement("User"));
        //   p.RequireRole("User");
    });

    builder.AddPolicy("Admin", p =>
    {
        p.AddAuthenticationSchemes("AdminScheme");
        p.AddRequirements(new MyAuthorizationRequirement("Admin"));
        // p.RequireRole("Admin");
    });
});


builder.Services.AddDbContext<MallContext>(p =>
{
    var con = builder.Configuration["Mysql:ConnectString"];
    var vs = builder.Configuration["MySql:Version"]!;
    var version = new MySqlServerVersion(new Version(vs));

    p.UseMySql(con, version);
    p.UseExceptionProcessor();
    p.UseLoggerFactory(LoggerFactory.Create(b => b.AddConsole()));
    p.EnableSensitiveDataLogging();
});




#endregion

#region 中间件
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthenFailHandling();
app.UseRouting();
app.UseCors("local");


app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
#endregion
