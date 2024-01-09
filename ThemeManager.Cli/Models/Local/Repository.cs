namespace ThemeManager.Cli.Models.Local;
public class Repository
{
    public string RemoteUrl { get; set; }
    public IEnumerable<Theme> Themes { get; set; } = Enumerable.Empty<Theme>();
}
