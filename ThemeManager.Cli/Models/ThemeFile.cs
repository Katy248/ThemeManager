using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThemeManager.Cli.Models;
public class ThemeFile
{
    public string Path { get; set; }
    public ThemeConfig Config { get; set; }
}
