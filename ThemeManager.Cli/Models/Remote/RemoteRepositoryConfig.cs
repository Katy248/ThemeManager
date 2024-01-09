namespace ThemeManager.Cli.Models.Remote;
public class RemoteRepositoryConfig
{
    public IEnumerable<RemoteTheme> Themes { get; set; } = Enumerable.Empty<RemoteTheme>();
}
