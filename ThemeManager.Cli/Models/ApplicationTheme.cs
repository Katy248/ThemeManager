using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThemeManager.Cli.Models;
public class ApplicationTheme
{
    public CopyOptions? CopyOptions { get; set; }
    public ReloadOptions? ReloadOptions { get; set; }
}
