using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThemeManager.Cli.Commands.Theme;
public class ThemeCommand : CliCommand, ITopLevelCliCommand
{
    private readonly AddThemeCommand _addThemeCommand;
    private readonly RemoveThemeCommand _removeThemeCommand;
    private readonly SetThemeCommand _setThemeCommand;

    public ThemeCommand(AddThemeCommand addThemeCommand, RemoveThemeCommand removeThemeCommand, SetThemeCommand setThemeCommand)
    {
        _addThemeCommand = addThemeCommand;
        _removeThemeCommand = removeThemeCommand;
        _setThemeCommand = setThemeCommand;
    }
    public override Command GetCommand()
    {
        var command = new Command("theme", "Manage themes.");

        command.AddCommand(_addThemeCommand);
        command.AddCommand(_removeThemeCommand);
        command.AddCommand(_setThemeCommand);

        return command;
    }
}
