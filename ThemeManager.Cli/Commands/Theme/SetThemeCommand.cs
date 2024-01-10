using System.CommandLine;

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
        var nameOption = new Option<string>(["--name", "-n"], "Name of theme to set.")
        {
            IsRequired = true,
        };
        var repositoryOption = new Option<string?>(["--repository", "-r"], "Repository to search theme from")
        {
            IsRequired = false
        };
        var command = new Command("set", "Sets current theme to specified.")
        {
            nameOption, repositoryOption
        };

        command.SetHandler(async (name, repository) => _themeManager.SetTheme(name, repository), nameOption, repositoryOption);

        return command;
    }
}
