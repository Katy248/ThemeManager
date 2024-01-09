using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThemeManager.Cli.Models.Remote;
public class RemoteTheme
{
    public string Name { get; set; }
    public Dictionary<string, string> ApplicationThemes { get; set; } = new();
}
