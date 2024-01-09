using System.Management.Automation;
using ThemeManager.Cli.FileSystem;
using ThemeManager.Cli.Models;
using Repository = ThemeManager.Cli.Models.Repository;

namespace ThemeManager.Cli.Managers;
public class RepositoryManager
{
    public static readonly string RepositoriesFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".themes/.repositories");
    private readonly ConfigManager _configManager;

    public RepositoryManager(ConfigManager configManager)
    {
        _configManager = configManager;
    }
    public async Task AddRepository(string remoteUrl)
    {
        _configManager.AddRepository(remoteUrl);
        var newFolderName = new DirectoryInfo(Path.GetFullPath(Path.Combine(RepositoriesFolder, GetLocalFolderName(remoteUrl))))
            .EnsureEmpty();
        
        using var powerShell = PowerShell.Create();
        powerShell.AddScript($"git clone {remoteUrl} {newFolderName}");
        await powerShell.InvokeAsync();
    }
    public void RemoveRepository(string remoteUrl)
    {
        var repositoryDirectory = new DirectoryInfo(Path.GetFullPath(Path.Combine(RepositoriesFolder, GetLocalFolderName(remoteUrl))));
        repositoryDirectory.EnsureDeleted();

        _configManager.RemoveRepository(remoteUrl);
    }
    public IEnumerable<Repository> GetRepositories()
    {
        var repositoriesUrls = _configManager.GetApplicationConfiguration()
            .ThemeRepositories;


        foreach (var url in repositoriesUrls)
        {
            var repo = new Repository { RemoteUrl = url.Value };

            repo.Themes = Directory
                .GetFiles(Path.Combine(RepositoriesFolder, GetLocalFolderName(repo.RemoteUrl)))
                .Select(file => new LocalTheme { FullPath = file });

            yield return repo;
        }
    }
    private static string GetLocalFolderName(string remoteUrl) =>
        remoteUrl
            .Replace("\\", ".")
            .Replace("/", ".")
            .Replace(":", ".")
            .Replace(" ", ".")
            .Trim();
}
