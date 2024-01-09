using System.CommandLine;

namespace ThemeManager.Cli.Commands.Theme;
public class ThemeCommand : CliCommand, ITopLevelCliCommand
{
    private readonly SetThemeCommand _setThemeCommand;

    public ThemeCommand(SetThemeCommand setThemeCommand)
    {
        _setThemeCommand = setThemeCommand;
    }
    public override Command GetCommand()
    {
        var command = new Command("theme", "Manage themes.");

        command.AddCommand(_setThemeCommand);

        return command;
    }
}
