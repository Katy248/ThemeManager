using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThemeManager.Cli.Models;
public class LocalTheme
{
    public string FullPath { get; set; }
    public string Name => new FileInfo(FullPath).Name.Replace(".json", "");
}
