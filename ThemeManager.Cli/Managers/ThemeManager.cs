using System.Management.Automation;
using ThemeManager.Cli.FileSystem;

namespace ThemeManager.Cli.Managers;
public class ThemeManager
{
    public static readonly DirectoryInfo CurrentThemeDirectory = new(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".themes/current"));
    public static readonly string ThemesFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".themes");

    private readonly LocalRepositoryManager _localRepositoryManager;


    public ThemeManager(LocalRepositoryManager localRepositoryManager)
    {
        _localRepositoryManager = localRepositoryManager;
    }
    public void SetTheme(string name, Dictionary<string, string> repositories, string? repository = null)
    {
        var repositoriesToSearch = _localRepositoryManager.GetLocalRepositories();
        if (!string.IsNullOrWhiteSpace(repository))
        {
            var repositoryUrl = repositories
                .FirstOrDefault(r => r.Key == repository && r.Value == repository)
                .Value;

            repositoriesToSearch = repositoriesToSearch.Where(r => r.RemoteUrl == repositoryUrl);

            if (!repositoriesToSearch.Any())
            {
                // TODO: replace exception
                throw new Exception($"There no repository for \"{repository}\"");
            }
        }

        var themeConfig = repositoriesToSearch
            .SelectMany(r => r.Themes)
            .FirstOrDefault(t => t.Name == name) ?? throw new Exception($"There no theme for \"{name}\"");

        CurrentThemeDirectory.EnsureEmpty();

        foreach (var appTheme in themeConfig.ApplicationThemes)
        {
            var appThemeDirectory = new DirectoryInfo(Path.Combine(CurrentThemeDirectory.FullName, appTheme.Application))
                .EnsureCreated();

            using var powerShell = PowerShell.Create();
            powerShell.AddScript($"git clone {appTheme.RemoteUrl} {appThemeDirectory.FullName}");
            powerShell.Invoke();
        }
    }
}
