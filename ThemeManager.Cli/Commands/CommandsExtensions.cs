using Microsoft.Extensions.DependencyInjection;
using ThemeManager.Cli.Commands.Configuration;
using ThemeManager.Cli.Commands.Repository;
using ThemeManager.Cli.Commands.Theme;

namespace ThemeManager.Cli.Commands;
public static class CommandsExtensions
{
    public static IServiceCollection AddCommands(this IServiceCollection services) =>
        services
        .AddTransient<ITopLevelCliCommand, RepositoryCommand>()
        .AddTransient<AddRepositoryCommand>()
        .AddTransient<RemoveRepositoryCommand>()
        .AddTransient<ListRepositoryCommand>()
        .AddTransient<ITopLevelCliCommand, ConfigurationCommand>()
        .AddTransient<CreateConfigurationCommand>()
        .AddTransient<ReloadConfigurationCommand>()
        .AddTransient<ITopLevelCliCommand, ThemeCommand>()
        .AddTransient<AddThemeCommand>()
        .AddTransient<SetThemeCommand>()
        .AddTransient<RemoveThemeCommand>()
        ;
}
