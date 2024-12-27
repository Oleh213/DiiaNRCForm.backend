using System.Reflection;
using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace DiiaNRCForm.Infrastructure.MediatR;

public static class MediatRServiceCollectionExtensions
{
    public static void AddMediatRInfrastructure(this IServiceCollection services, params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
        }

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(CommandLoggingProcessor<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }
}
