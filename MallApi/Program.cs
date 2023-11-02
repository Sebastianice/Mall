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

builder.Services.AddControllers(opts => {
    opts.Filters.Add(new GExceptionFilter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(g => {
    var scheme = new OpenApiSecurityScheme() {
        Description = "Authorization header. \r\nExample: 'Bearer 12345abcdef'",
        Reference = new OpenApiReference {
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
var Issurer = builder.Configuration["TokenParameters:iss"];  //������
var Audience = builder.Configuration["TokenParameters:aud"];       //������
var secretCredentials = builder.Configuration["TokenParameters:sign"];
builder.Services.AddAuthentication(x => {
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o => {
    o.TokenValidationParameters = new TokenValidationParameters {
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


builder.Services.AddDbContext<MallContext>(p => {
 
    var version = new MySqlServerVersion(new Version(vs));
    p.UseMySql(con, version);
    p.UseExceptionProcessor();
    p.UseLoggerFactory(LoggerFactory.Create(b => b.AddConsole()));
});





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
