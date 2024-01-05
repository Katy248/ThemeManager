using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThemeManager.Cli.Models;
public class Repository
{
    public string RemoteUrl { get; set; }
    public IEnumerable<LocalTheme> Themes { get; set; } = Enumerable.Empty<LocalTheme>();
}
