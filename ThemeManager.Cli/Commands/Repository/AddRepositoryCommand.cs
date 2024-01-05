using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThemeManager.Cli.Managers;

namespace ThemeManager.Cli.Commands.Repository;
public class AddRepositoryCommand : CliCommand
{
    private readonly RepositoryManager _manager;

    public AddRepositoryCommand(RepositoryManager manager)
    {
        _manager = manager;
    }
    public override Command GetCommand()
    {
        var repositoryOption = new Option<Uri>("--uri", "Link to remote repository");
        repositoryOption.AddAlias("-u");
        var command = new Command("add", "Add new repository to search theme from.")
        {
            repositoryOption
        };

        command.SetHandler(async (uri) => await _manager.AddRepository(uri.ToString()), repositoryOption);


        return command;
    }
    
}
