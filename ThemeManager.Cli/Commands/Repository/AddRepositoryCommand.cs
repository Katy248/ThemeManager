using System.CommandLine;
using System.Runtime.CompilerServices;
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
        var repositoryArg = new Argument<Uri>("uri", "Link to remote repository");
        var nameOption = new Option<string?>(aliases: ["--name", "-n"], description: "Name of new repository")
        {
            IsRequired = false,
        };

        var command = new Command("add", "Add new repository to search theme from.")
        {
            repositoryArg,
            nameOption,
        };

        command.SetHandler((uri, name) => 
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                name = uri.ToString();
            }
            _configManager.AddRepository(uri.ToString(), name);
        }, repositoryArg, nameOption);

        return command;
    }

}
