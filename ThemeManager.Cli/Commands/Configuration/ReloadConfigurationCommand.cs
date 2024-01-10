using System.CommandLine;
using ThemeManager.Cli.Managers;

namespace ThemeManager.Cli.Commands.Configuration;
public class ReloadConfigurationCommand : CliCommand
{
    private readonly ConfigManager _manager;

    public ReloadConfigurationCommand(ConfigManager manager)
    {
        _manager = manager;
    }
    public override Command GetCommand()
    {
        var command = new Command("reload", "Apply all changes to configuration file.");

        command.SetHandler(() => _manager.Reload());

        return command;
    }
}
