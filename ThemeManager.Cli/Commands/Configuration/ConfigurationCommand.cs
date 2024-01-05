using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThemeManager.Cli.Commands.Configuration;
public class ConfigurationCommand : CliCommand, ITopLevelCliCommand
{
    private readonly CreateConfigurationCommand _createConfigurationCommand;
    private readonly ReloadConfigurationCommand _reloadConfigurationCommand;

    public ConfigurationCommand(CreateConfigurationCommand createConfigurationCommand, ReloadConfigurationCommand reloadConfigurationCommand)
    {
        _createConfigurationCommand = createConfigurationCommand;
        _reloadConfigurationCommand = reloadConfigurationCommand;
    }
    public override Command GetCommand()
    {
        var command = new Command("configuration", "Manage configuration file.");
        command.AddAlias("config");

        command.AddCommand(_createConfigurationCommand);
        command.AddCommand(_reloadConfigurationCommand);

        return command;
    }
}
