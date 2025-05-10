namespace Blazinga.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection RegisterBlazingaServices(this IServiceCollection services)
    {
        services.AddScoped<NavMenuStateService>();
        services.AddSingleton<ThemeService>();

        return services;
    }
}
