using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThemeManager.Cli.Commands.Repository;
public class RepositoryCommand : CliCommand, ITopLevelCliCommand
{
    private readonly AddRepositoryCommand _addRepositoryCommand;
    private readonly RemoveRepositoryCommand _removeRepositoryCommand;
    private readonly ListRepositoryCommand _listRepositoryCommand;

    public RepositoryCommand(AddRepositoryCommand addRepositoryCommand, RemoveRepositoryCommand removeRepositoryCommand, ListRepositoryCommand listRepositoryCommand)
    {
        _addRepositoryCommand = addRepositoryCommand;
        _removeRepositoryCommand = removeRepositoryCommand;
        _listRepositoryCommand = listRepositoryCommand;
    }

    public override Command GetCommand()
    {
        var command = new Command("repository", "Mange repositories.");
        command.AddAlias("repo");
        command.AddCommand(_addRepositoryCommand);
        command.AddCommand(_removeRepositoryCommand);
        command.AddCommand(_listRepositoryCommand);

        return command;
    }
}
