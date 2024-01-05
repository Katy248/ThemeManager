using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThemeManager.Cli.Models.Configuration;
public sealed class ApplicationConfiguration
{
    public string? CurrentTheme { get; set; }
    public List<string> ThemeRepositories { get; set; } = new();
    public IEnumerable<string> CustomThemes { get; set; } = Enumerable.Empty<string>();
}
