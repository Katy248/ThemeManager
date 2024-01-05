using System.CommandLine;
using Microsoft.Extensions.DependencyInjection;
using ThemeManager.Cli.Commands;
using ThemeManager.Cli.Managers;

var services = new ServiceCollection()
    .AddManagers()
    .AddCommands()
    .BuildServiceProvider();

var rootCommand = new RootCommand(description: "Theme management application");

foreach (var command in services.GetRequiredService<IEnumerable<ITopLevelCliCommand>>())
{
    rootCommand.AddCommand(command.GetCommand());
}

await rootCommand.InvokeAsync(args);
