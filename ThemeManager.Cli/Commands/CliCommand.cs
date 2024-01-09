using System.CommandLine;

namespace ThemeManager.Cli.Commands;
public abstract class CliCommand : ICliCommand
{
    public abstract Command GetCommand();

    public static implicit operator Command(CliCommand cliCommand) => cliCommand.GetCommand();
}
