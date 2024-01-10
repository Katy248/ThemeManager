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
        var repositoryOption = new Option<Uri>(aliases: ["--uri", "-u"], "Link to remote repository")
        {
            IsRequired = true,
            Arity = ArgumentArity.ExactlyOne
        };
        var nameOption = new Option<string?>(aliases: ["--name", "-n"], description: "Name of new repository")
        {
            IsRequired = false,
        };

        var command = new Command("add", "Add new repository to search theme from.")
        {
            repositoryOption,
            nameOption,
        };

        command.SetHandler((uri, name) => 
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                name = uri.ToString();
            }
            _configManager.AddRepository(uri.ToString(), name);
        }, repositoryOption, nameOption);

        return command;
    }

}
