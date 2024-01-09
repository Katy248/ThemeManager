using System.CommandLine;

namespace ThemeManager.Cli.Commands.Configuration;
public class ReloadConfigurationCommand : CliCommand
{
    public override Command GetCommand()
    {
        var command = new Command("reload", "Apply all changes to configuration file.");

        return command;
    }
}
