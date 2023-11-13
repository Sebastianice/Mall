using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("�����С���");

try
{
    var builder = WebApplication.CreateBuilder(args);

    var configuration = builder.Configuration;

    // ������������
    builder.SimpleConfigure();

    // API
    builder.Services.AddControllers()
    .AddDataValidation() //ģ�Ͳ���У��
    .AddAppResult(options =>
    {
        options.ResultFactory = resultException =>
        {
            // AppResultException ������ 200 ״̬��
            var objectResult = new ObjectResult(resultException.AppResult);
            objectResult.StatusCode = StatusCodes.Status200OK;
            return objectResult;
        };
    });


    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();


    // Swagger
    builder.Services.AddSimpleSwagger(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "Mall�̳ǽӿ��ĵ�v1", Version = "v1" });
    });

    // �ִ���
    builder.Services.AddRepository(configuration["MySql:ConnectStrings"]);


    // ����㣺�Զ���� Service ���� Service ��β�ķ���
    builder.Services.AddAutoServices("Mall.Services");

    // JWT ��֤
    builder.Services.AddJwtAuthentication();

    // ��Ȩ
    builder.Services.AddSimpleAuthorization();


    // ����
    builder.Services.AddSimpleCors();



    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseCors("local");

    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "staticfiles")),
        RequestPath = "/staticfiles"
    });


    app.UseAuthorization();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "���ڷ����쳣�����³�����ֹ��");
    throw;
}
finally
{
    LogManager.Shutdown();
}