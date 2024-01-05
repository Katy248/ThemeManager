using System.CommandLine;
using ThemeManager.Cli.Managers;

namespace ThemeManager.Cli.Commands.Repository;
public class ListRepositoryCommand : CliCommand
{
    private readonly RepositoryManager _manager;

    public ListRepositoryCommand(RepositoryManager manager)
    {
        _manager = manager;
    }
    public override Command GetCommand()
    {
        var command = new Command("list", "Write list of local repositories.");

        command.SetHandler(async () =>
        {
            foreach (var repo in _manager.GetRepositories())
            {
                Console.WriteLine($"{repo.RemoteUrl}");

                foreach (var theme in repo.Themes)
                {
                    Console.WriteLine($"\t{theme.Name}");
                }
            }
        });

        return command;
    }
}
