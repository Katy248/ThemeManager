using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThemeManager.Cli;
public class CliParserBuilder
{
    private readonly IServiceProvider _serviceProvider;
    private readonly RootCommand _rootCommand = new();
    public CliParserBuilder(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task<int> Run(string[] args)
    {
        return _rootCommand.InvokeAsync(args);
    }
}
