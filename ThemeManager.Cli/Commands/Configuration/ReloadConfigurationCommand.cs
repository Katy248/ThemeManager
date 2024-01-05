using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThemeManager.Cli.Commands.Configuration;
public class ReloadConfigurationCommand : CliCommand
{
    public override Command GetCommand()
    {
        var command = new Command("reload", "Apply all changes to configuration file.");

        return command;
    }
}
