using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThemeManager.Cli.Managers;

namespace ThemeManager.Cli.Commands.Theme;
public class AddThemeCommand : CliCommand
{
    private readonly Managers.ThemeManager _themeManager;

    public AddThemeCommand(Managers.ThemeManager themeManager)
    {
        _themeManager = themeManager;
    }
    public override Command GetCommand()
    {
        var nameOption = new Option<string>("--name", "Specify theme name.");
        nameOption.AddAlias("-n");
        var repositoryOption = new Option<string>(
            name: "--repository", 
            description: "Specify repository from which will be downloaded theme.",
            getDefaultValue: () => "");
        repositoryOption.AddAlias("-r");

        var command = new Command("add", "Download remote theme.")
        {
            nameOption,
            repositoryOption
        };

        command.SetHandler(async (name, repo) => _themeManager.AddTheme(name), nameOption, repositoryOption);

        return command;
    }
}
