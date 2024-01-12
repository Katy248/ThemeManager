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
        var repositoryArg = new Argument<string>("repository", "Name or url of repository");
        var command = new Command("remove", "Remove local files of remote repository.")
        {
            repositoryArg
        };
        command.AddAlias("rm");

        command.SetHandler((repository) => _manager.RemoveRepository(repository.ToString()), repositoryArg);

        return command;
    }
}
