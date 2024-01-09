using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThemeManager.Cli.Commands.Theme;
public class RemoveThemeCommand : CliCommand
{
    private readonly Managers.ThemeManager _themeManager;

    public RemoveThemeCommand(Managers.ThemeManager themeManager)
    {
        _themeManager = themeManager;
    }
    public override Command GetCommand()
    {
        var nameOption = new Option<string>("--name");
        nameOption.AddAlias("-n");
        var command = new Command("remove", "Remove local theme files.")
        {
            nameOption
        };
        command.AddAlias("rm");

        //command.SetHandler(async (name) => _themeManager.RemoveTheme(name), nameOption);

        return command;
    }
}
