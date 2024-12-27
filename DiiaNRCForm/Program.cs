using DiiaNRCForm.Abstractions.AppSettings;
using DiiaNRCForm.Business;
using DiiaNRCForm.DiiaService;
using DiiaNRCForm.Infrastructure.Database;
using DiiaNRCForm.Infrastructure.MediatR;
using DiiaNRCForm.Infrastructure.SignalR;
using DiiaNRCForm.Infrastructure.SignalR.Service;
using DiiaNRCForm.KoboToolboxService;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var appSettings = BindAppSettings(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "NRC API",
        Version = "v1"
    });
});

var assemblies = new[]
{
    typeof(Program).Assembly,
    typeof(BusinessServiceCollectionExtensions).Assembly,
};

builder.Services.AddSingleton(appSettings);

builder.Services.AddPostgresInfrastructure(appSettings);
builder.Services.AddPostgresInfrastructure(appSettings);
builder.Services.AddSignalRInfrastructure();
builder.Services.AddKoboToolboxServiceInfrastructure();
builder.Services.AddDiiaServiceInfrastructure(appSettings);
builder.Services.AddBusiness(assemblies);
builder.Services.AddMediatRInfrastructure(assemblies);

var app = builder.Build();

//todo: uncommit in prod
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "NRC API API");
        c.RoutePrefix = string.Empty;  
    });
// }

app.MapHub<SignatureHub>(SignatureHub.HubDirection,
        options => { options.Transports = HttpTransportType.WebSockets; });


app.Use(async (context, next) =>
{
    await next();
    var path = context.Request.Path.Value!;
    if (context.Response.StatusCode == 404 &&
        !path.StartsWith("/api/"))
    {
        context.Request.Path = "/index.html";
        await next();
    }
});

app.UseRouting();

app.UseCors(builder =>
    builder.WithOrigins(appSettings.OriginUrl)
        .SetIsOriginAllowed(origin => true)
        .AllowAnyHeader()
        .AllowCredentials()
        .AllowAnyMethod());

var logger = app.Logger;

ApplyMigrationsAndSeeding(app, logger);

app.UseAuthorization();
app.MapControllers();

app.Run();


static void ApplyMigrationsAndSeeding(IApplicationBuilder app, ILogger logger)
{

    var scopeFactory = app.ApplicationServices.GetService<IServiceScopeFactory>();

    if (scopeFactory != null)
    {
        using var scope = scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<DiiaNRCFormDbContext>();
        
        context.Database.Migrate();
    }
}

static AppSettings BindAppSettings(IConfiguration configuration)
{
    var appSettings = new AppSettings();

    try
    {
        new ConfigureFromConfigurationOptions<AppSettings>(configuration).Configure(appSettings);
    }
    catch (Exception exception)
    {
        Console.WriteLine($"Error binding appsettings.json to AppSettings: {exception}");
        throw;
    }
    
    return appSettings;
}