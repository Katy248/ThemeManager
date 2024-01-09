using Microsoft.Extensions.DependencyInjection;

namespace ThemeManager.Cli.Managers;
public static class ManagersExtensions
{
    public static IServiceCollection AddManagers(this IServiceCollection services) =>
        services
        .AddSingleton<ConfigManager>()
        .AddSingleton<ThemeManager>()
        .AddSingleton<ApplicationsManager>()
        .AddSingleton<LocalRepositoryManager>()
        ;
}
