namespace Microservice.UserManager.Modules.Injection;

public static class InjectionExtensions
{
    public static IServiceCollection AddInjection(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddSingleton<IConfiguration>(configuration);
        
        return services;
    }
}
