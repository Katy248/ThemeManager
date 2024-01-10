using System.CommandLine;
using ThemeManager.Cli.Managers;

namespace ThemeManager.Cli.Commands.Repository;
public class RemoveRepositoryCommand : CliCommand
{
    private readonly ConfigManager _manager;

    public RemoveRepositoryCommand(ConfigManager manager)
    {
        _manager = manager;
    }
    public override Command GetCommand()
    {
        var repositoryOption = new Option<string>(["--repository", "-r"], "Name or url of repository")
        {
            IsRequired = true,
        };
        var command = new Command("remove", "Remove local files of remote repository.")
        {
            repositoryOption
        };
        command.AddAlias("rm");

        command.SetHandler((repository) => _manager.RemoveRepository(repository.ToString()), repositoryOption);

        return command;
    }
}
