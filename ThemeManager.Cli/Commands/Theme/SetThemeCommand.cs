using System.CommandLine;
using ThemeManager.Cli.Managers;

namespace ThemeManager.Cli.Commands.Theme;
public class SetThemeCommand : CliCommand
{
    private readonly ConfigManager _configManager;

    public SetThemeCommand(ConfigManager configManager)
    {
        _configManager = configManager;
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

        command.SetHandler((name, repository) => _configManager.SetTheme(name, repository), nameOption, repositoryOption);

        return command;
    }
}
