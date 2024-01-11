using System.CommandLine;
using Microsoft.Extensions.Logging;
using ThemeManager.Cli.Managers;

namespace ThemeManager.Cli.Commands.Configuration;
public class ReloadConfigurationCommand : CliCommand
{
    private readonly ILogger<ReloadConfigurationCommand> _logger;
    private readonly ConfigManager _manager;

    public ReloadConfigurationCommand(ILogger<ReloadConfigurationCommand> logger, ConfigManager manager)
    {
        _logger = logger;
        _manager = manager;
    }
    public override Command GetCommand()
    {
        var command = new Command("reload", "Apply all changes to configuration file.");

        command.SetHandler(() =>
        {
            _logger.LogDebug($"Handled reload configuration command");
            _manager.Reload();
        });

        return command;
    }
}
