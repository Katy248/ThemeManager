namespace ThemeManager.Cli.Models;
public class LocalTheme
{
    public string FullPath { get; set; }
    public string Name => new FileInfo(FullPath).Name.Replace(".json", "");
}
