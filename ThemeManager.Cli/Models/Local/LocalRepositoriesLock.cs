namespace ThemeManager.Cli.Models.Local;
public class LocalRepositoriesLock
{
    public IEnumerable<Repository> Repositories { get; set; } = Enumerable.Empty<Repository>();
}
