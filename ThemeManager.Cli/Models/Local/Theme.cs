namespace ThemeManager.Cli.Models.Local;

public class Theme
{
    public string Name { get; set; }
    public IEnumerable<ApplicationTheme> ApplicationThemes { get; set; } = Enumerable.Empty<ApplicationTheme>();
}