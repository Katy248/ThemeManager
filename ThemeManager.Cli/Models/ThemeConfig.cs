using Newtonsoft.Json;

namespace ThemeManager.Cli.Models;

public class ThemeConfig
{
    private string? _name;
    public string Name { get; set; }
    public string Decription { get; set; }
    public Dictionary<string, string> ApplicationThemes { get; set; }
}