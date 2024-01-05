using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThemeManager.Cli.Commands;
public abstract class CliCommand : ICliCommand
{
    public abstract Command GetCommand();

    public static implicit operator Command(CliCommand cliCommand) => cliCommand.GetCommand();
}
