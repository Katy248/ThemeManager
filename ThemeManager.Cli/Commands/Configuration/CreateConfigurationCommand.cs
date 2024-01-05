using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThemeManager.Cli.Commands.Configuration;
public class CreateConfigurationCommand : CliCommand
{
    public override Command GetCommand()
    {
        var command = new Command("create", "Creates configuration file if it isn't exists.");

        return command;
    }
}
