namespace ThemeManager.Cli.Models.Remote;
public class RemoteTheme
{
    public string Name { get; set; }
    public Dictionary<string, string> ApplicationThemes { get; set; } = new();
}
