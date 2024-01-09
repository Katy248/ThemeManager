using System.CommandLine;
using ThemeManager.Cli.Managers;

namespace ThemeManager.Cli.Commands.Repository;
public class AddRepositoryCommand : CliCommand
{
    private readonly LocalRepositoryManager _manager;
    private readonly ConfigManager _configManager;

    public AddRepositoryCommand(LocalRepositoryManager manager, ConfigManager configManager)
    {
        _manager = manager;
        _configManager = configManager;
    }
    public override Command GetCommand()
    {
        var repositoryOption = new Option<Uri>("--uri", "Link to remote repository");
        var nameOption = new Option<string?>("--name", "Name of new repository");
        repositoryOption.AddAlias("-u");
        nameOption.AddAlias("-n");
        var command = new Command("add", "Add new repository to search theme from.")
        {
            repositoryOption,
            nameOption,
        };

        command.SetHandler((uri, name) => _configManager.AddRepository(uri.ToString(), name), repositoryOption, nameOption);

        return command;
    }

}
