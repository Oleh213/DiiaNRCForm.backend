using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace DiiaNRCForm.Business;

public static class BusinessServiceCollectionExtensions
{
    public static void AddBusiness(this IServiceCollection services, params Assembly[] assemblies)
    {
        // var profileAssembly = typeof(EntityToModelMappingProfile).Assembly;
        // services.AddAutoMapper(profileAssembly);
        //
        // services.AddRules(assemblies);
        // services.AddValidators(assemblies);
        // services.AddScoped<IAuth0UserContext, Auth0UserContext>();
        // services.AddScoped<IShopRater, ShopRater>();
    }
}
