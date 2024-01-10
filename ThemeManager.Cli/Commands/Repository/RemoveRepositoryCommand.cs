using System.CommandLine;
using ThemeManager.Cli.Managers;

namespace ThemeManager.Cli.Commands.Repository;
public class RemoveRepositoryCommand : CliCommand
{
    private readonly LocalRepositoryManager _manager;

    public RemoveRepositoryCommand(LocalRepositoryManager manager)
    {
        _manager = manager;
    }
    public override Command GetCommand()
    {
        var repositoryOption = new Option<Uri>(["--url", "-u"], "Link to remote repository.")
        {
            IsRequired = true,
        };
        var command = new Command("remove", "Remove local files of remote repository.")
        {
            repositoryOption
        };
        command.AddAlias("rm");


        command.SetHandler((uri) => _manager.RemoveRepository(uri.ToString()), repositoryOption);

        return command;
    }
}
