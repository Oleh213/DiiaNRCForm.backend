using System.Text;
using DiiaNRCForm.Abstractions.AppSettings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace DiiaNRCForm.Controllers.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class BasicAuthAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var configuration = context.HttpContext.RequestServices.GetService<IConfiguration>();
        
        var appSettings = new AppSettings();
        
        new ConfigureFromConfigurationOptions<AppSettings>(configuration).Configure(appSettings);
        
        if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
        {
            context.Result = new UnauthorizedObjectResult("Authorization header is missing.");
            return;
        }

        if (!ValidateBasicAuth(authorizationHeader, appSettings.KoboToolboxSettings.EndpointUsername, appSettings.KoboToolboxSettings.EndpointPassword))
        {
            context.Result = new UnauthorizedObjectResult("Invalid credentials.");
            return;
        }

        await next();
    }

    private bool ValidateBasicAuth(string authorizationHeader, string username, string password)
    {
        if (!authorizationHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        var encodedCredentials = authorizationHeader.Substring("Basic ".Length).Trim();
        var decodedCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials));

        var credentials = decodedCredentials.Split(':');
        if (credentials.Length != 2)
        {
            return false;
        }

        return credentials[0] == username && credentials[1] == password;
    }
}