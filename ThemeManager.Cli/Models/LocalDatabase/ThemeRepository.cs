namespace ThemeManager.Cli.Models.LocalDatabase;

public class ThemeRepository
{
    public string RemouteUrl { get; set; }
    public IEnumerable<Theme> Themes { get; set; }
}
