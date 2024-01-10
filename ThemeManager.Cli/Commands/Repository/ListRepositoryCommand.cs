using System.CommandLine;
using ThemeManager.Cli.Managers;

namespace ThemeManager.Cli.Commands.Repository;
public class ListRepositoryCommand : CliCommand
{
    private readonly LocalRepositoryManager _manager;

    public ListRepositoryCommand(LocalRepositoryManager manager)
    {
        _manager = manager;
    }
    public override Command GetCommand()
    {
        var command = new Command("list", "Write list of local repositories.");

        command.SetHandler(() =>
        {
            foreach (var repo in _manager.GetLocalRepositories())
            {
                Console.WriteLine($"{repo.RemoteUrl}");

                foreach (var theme in repo.Themes)
                {
                    Console.WriteLine($"\t- {theme.Name}");

                    foreach (var appTheme in theme.ApplicationThemes)
                    {
                        Console.WriteLine($"\t\t- {appTheme.Application}");
                    }
                }
            }
        });

        return command;
    }
}
