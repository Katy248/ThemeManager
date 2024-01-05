using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThemeManager.Cli.Commands.Theme;
public class SetThemeCommand : CliCommand
{
    private readonly Managers.ThemeManager _themeManager;

    public SetThemeCommand(Managers.ThemeManager themeManager)
    {
        _themeManager = themeManager;
    }
    public override Command GetCommand()
    {
        var nameOption = new Option<string>("--name", "Name of theme to set.");
        nameOption.AddAlias("-n");
        var command = new Command("set", "Sets current theme to specified.")
        {
            nameOption
        };

        command.SetHandler(async (name) => _themeManager.SetTheme(name), nameOption);

        return command;
    }
}
