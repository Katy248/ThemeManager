using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace ThemeManager.Cli.Managers;
public static class ManagersExtensions
{
    public static IServiceCollection AddManagers(this IServiceCollection services) =>
        services
        .AddSingleton<RepositoryManager>()
        .AddSingleton<ConfigManager>()
        .AddSingleton<FileSystemManager>()
        .AddSingleton<ThemeManager>()
        .AddSingleton<ApplicationsManager>()
        .AddSingleton<LocalThemeManager>()
        .AddSingleton<DownloadThemeManager>()
        ;
}
