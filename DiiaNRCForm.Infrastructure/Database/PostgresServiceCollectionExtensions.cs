using DiiaNRCForm.Abstractions.AppSettings;
using DiiaNRCForm.Abstractions.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DiiaNRCForm.Infrastructure.Database;

public static class PostgresServiceCollectionExtensions
{
    public static void AddPostgresInfrastructure(this IServiceCollection services, AppSettings appSettings)
    {
        services.AddDbContext<DiiaNRCFormDbContext>(options =>
        {
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .UseNpgsql(appSettings.PostgresNRCFromDb,
                    x => x.UseNetTopologySuite());
        
            options.UseSnakeCaseNamingConvention();
        });
        
        services.AddScoped<IDiiaNRCFormDbContext>(provider => provider.GetService<DiiaNRCFormDbContext>());
    }
}
