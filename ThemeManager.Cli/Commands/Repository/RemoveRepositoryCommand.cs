using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        var repositoryOption = new Option<Uri>("--uri", "Link to remote repository.");
        repositoryOption.AddAlias("-u");
        var command = new Command("remove", "Remove local files of remote repository.")
        {
            repositoryOption
        };
        command.AddAlias("rm");


        command.SetHandler((uri) => _manager.RemoveRepository(uri.ToString()), repositoryOption);

        return command;
    }
}
