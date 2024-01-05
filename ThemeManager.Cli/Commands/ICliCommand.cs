using System.CommandLine;

namespace ThemeManager.Cli.Commands;
public interface ICliCommand
{
    Command GetCommand();
}
