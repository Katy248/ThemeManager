namespace ThemeManager.Cli.Models;
public class Repository
{
    public string RemoteUrl { get; set; }
    public IEnumerable<LocalTheme> Themes { get; set; } = Enumerable.Empty<LocalTheme>();
}
