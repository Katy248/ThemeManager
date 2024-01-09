using System.CommandLine;

namespace ThemeManager.Cli.Commands.Configuration;
public class CreateConfigurationCommand : CliCommand
{
    public override Command GetCommand()
    {
        var command = new Command("create", "Creates configuration file if it isn't exists.");

        return command;
    }
}
