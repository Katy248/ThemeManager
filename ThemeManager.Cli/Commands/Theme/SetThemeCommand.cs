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
        var nameArg = new Argument<string>("name", "Name of theme to set");
        var repositoryOption = new Option<string?>(["--repository", "-r"], "Repository to search theme from")
        {
            IsRequired = false
        };
        var command = new Command("set", "Sets current theme to specified.")
        {
            nameArg, repositoryOption
        };

        command.SetHandler((name, repository) => _configManager.SetTheme(name, repository), nameArg, repositoryOption);

        return command;
    }
}
