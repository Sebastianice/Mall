using System.IdentityModel.Tokens.Jwt;
using System.Text;
using EntityFramework.Exceptions.MySQL;
using MallApi.filter;
using MallDomain.service.mall;
using MallInfrastructure;
using MallInfrastructure.service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(opts =>
{
    opts.Filters.Add(typeof(GExceptionFilter));

});
//ʹ��servicefilter�в�ע��tokenfilter
builder.Services.AddScoped(typeof(TokenFilter));


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


var con = builder.Configuration["Mysql:ConnectString"];
var vs = builder.Configuration["MySql:Version"]!;

builder.Services.AddScoped<IMallGoodsCategoryService, MallGoodsCategoryService>();
builder.Services.AddScoped<IMallGoodsInfoService, MallGoodsInfoService>();
builder.Services.AddScoped<IMallCarouselService, MallCarouselService>();
builder.Services.AddScoped<IMallIndexInfoService, MallIndexInfoService>();
builder.Services.AddScoped<IMallOrderService, MallOrderService>();
builder.Services.AddScoped<IMallUserService, MallUserService>();
builder.Services.AddSingleton<JwtSecurityTokenHandler>();
builder.Services.AddScoped<IMallShopCartService, MallShopCartService>();
builder.Services.AddScoped<IMallUserAddressService, MallUserAddressService>();
builder.Services.AddScoped<IMallUserTokenService, MallUserTokenService>();


builder.Services.AddMemoryCache();
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer("UserScheme", o =>
{
    var Issurer = builder.Configuration["UserToken:iss"];  //������
    var Audience = builder.Configuration["UserToken:aud"];       //������
    var secretCredentials = builder.Configuration["UserToken:sign"];
    o.TokenValidationParameters = new TokenValidationParameters
    {

        //�Ƿ���֤������
        ValidateIssuer = true,
        ValidIssuer = Issurer,//������
                              //�Ƿ���֤������
        ValidateAudience = true,
        ValidAudience = Audience,//������
                                 //�Ƿ���֤��Կ
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretCredentials)),

        ValidateLifetime = true, //��֤��������
        RequireExpirationTime = true, //����ʱ��
    };
}).AddJwtBearer("AdminScheme", o =>
{

    var Issurer = builder.Configuration["AdminToken:iss"];  //������
    var Audience = builder.Configuration["AdminToken:aud"];       //������
    var secretCredentials = builder.Configuration["AdminToken:sign"];
    o.TokenValidationParameters = new TokenValidationParameters
    {
        //�Ƿ���֤������
        ValidateIssuer = true,
        ValidIssuer = Issurer,//������
                              //�Ƿ���֤������
        ValidateAudience = true,
        ValidAudience = Audience,//������
                                 //�Ƿ���֤��Կ
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretCredentials)),

        ValidateLifetime = true, //��֤��������
        RequireExpirationTime = true, //����ʱ��
    };

});

builder.Services.AddAuthorization(builder =>
{
    builder.AddPolicy("UserPolicy", p =>
    {

        p.AddAuthenticationSchemes("UserScheme");
        p.RequireRole("User");

    });
    builder.AddPolicy("AdminPolicy", p =>
    {
        p.AddAuthenticationSchemes("AdminScheme");
        p.RequireRole("Admin");
    });
});

builder.Services.AddDbContext<MallContext>(p =>
{

    var version = new MySqlServerVersion(new Version(vs));
    p.UseMySql(con, version);
    p.UseExceptionProcessor();
    p.UseLoggerFactory(LoggerFactory.Create(b => b.AddConsole()));
});





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
