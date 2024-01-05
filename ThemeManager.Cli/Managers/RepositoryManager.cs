using System.Management.Automation;
using ThemeManager.Cli.Models;
using Repository = ThemeManager.Cli.Models.Repository;

namespace ThemeManager.Cli.Managers;
public class RepositoryManager
{
    public static readonly string RepositoriesFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".themes/.repositories");
    private readonly ConfigManager _configManager;
    private readonly FileSystemManager _fileSystemManager;

    public RepositoryManager(ConfigManager configManager, FileSystemManager fileSystemManager)
    {
        _configManager = configManager;
        _fileSystemManager = fileSystemManager;
    }
    public async Task AddRepository(string remoteUrl)
    {
        _configManager.AddRepository(remoteUrl);
        var newFolderName = Path.GetFullPath(Path.Combine(RepositoriesFolder, GetLocalFolderName(remoteUrl)));
        _fileSystemManager.EnsureDelete(newFolderName);

        Directory.CreateDirectory(newFolderName);
        
        using var powerShell = PowerShell.Create();
        powerShell.AddScript($"git clone {remoteUrl} {newFolderName}");
        await powerShell.InvokeAsync();
    }
    public void RemoveRepository(string remoteUrl)
    {
        var folderName = Path.GetFullPath(Path.Combine(RepositoriesFolder, GetLocalFolderName(remoteUrl)));
        _fileSystemManager.EnsureDelete(folderName);

        _configManager.RemoveRepository(remoteUrl);
    }
    public IEnumerable<Repository> GetRepositories()
    {
        var repositoriesUrls = _configManager.GetApplicationConfiguration()
            .ThemeRepositories;


        foreach (var url in repositoriesUrls)
        {
            var repo = new Repository { RemoteUrl = url };

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
