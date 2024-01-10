using System.Management.Automation;
using Newtonsoft.Json;
using ThemeManager.Cli.FileSystem;
using ThemeManager.Cli.Models.Local;
using ThemeManager.Cli.Models.Remote;

namespace ThemeManager.Cli.Managers;
public class LocalRepositoryManager
{
    public static readonly string RepositoriesDirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".themes/.repositories");
    public static readonly FileInfo RepositoriesLockFile = new(Path.Combine(RepositoriesDirectoryPath, "repositories.lock.json"));
    public static readonly LocalRepositoriesLock DefaultRepositoriesLock = new();

    public void AddRepository(string remoteUrl)
    {
        var temporaryDirectory = new DirectoryInfo(Path.GetFullPath(Path.Combine(RepositoriesDirectoryPath, "temp")))
            .EnsureEmpty();

        using var powerShell = PowerShell.Create();
        powerShell.AddScript($"git clone {remoteUrl} {temporaryDirectory.FullName}");
        powerShell.Invoke();

        var remoteConfig = RemoteRepositoryConfig.FromFile(temporaryDirectory.Child("config.json"));

        if (remoteConfig == RemoteRepositoryConfig.Default)
            return; // TODO: Add special message

        var repositoriesLock = GetLocalRepositoriesLock();
        repositoriesLock.Repositories = repositoriesLock.Repositories.Where(r => r.RemoteUrl != remoteUrl);
        repositoriesLock.Repositories = repositoriesLock.Repositories.Append(ToLocalRepository(remoteUrl.ToString(), remoteConfig));
        UpdateLocalRepositoriesLock(repositoriesLock);
    }
    public void RemoveRepository(string remoteUrl)
    {
        var config = GetLocalRepositoriesLock();

        config.Repositories = config.Repositories.Where(r => r.RemoteUrl != remoteUrl);

        UpdateLocalRepositoriesLock(config);
    }

    private Repository ToLocalRepository(string url, RemoteRepositoryConfig remoteConfig)
    {
        var localRepository = new Repository { RemoteUrl = url };
        var themes = new List<Theme>();
        foreach (var theme in remoteConfig.Themes)
        {
            var localTheme = new Theme
            {
                Name = theme.Name,
                ApplicationThemes = theme.ApplicationThemes
                .Select(keyValue => new ApplicationTheme
                {
                    Application = keyValue.Key,
                    RemoteUrl = keyValue.Value
                })
            };
            themes.Add(localTheme);
        }
        localRepository.Themes = themes;

        return localRepository;
    }

    public void RemoveAllRepositories()
    {
        var repositoriesLock = GetLocalRepositoriesLock();
        repositoriesLock.Repositories = Enumerable.Empty<Repository>();

        UpdateLocalRepositoriesLock(repositoriesLock);
    }

    public IEnumerable<Repository> GetLocalRepositories()
    {
        return GetLocalRepositoriesLock().Repositories;
    }

    private LocalRepositoriesLock GetLocalRepositoriesLock()
    {
        EnsureLockFileExists();

        var configText = File.ReadAllText(RepositoriesLockFile.FullName);

        return JsonConvert.DeserializeObject<LocalRepositoriesLock>(configText) ?? DefaultRepositoriesLock;
    }
    private void UpdateLocalRepositoriesLock(LocalRepositoriesLock localRepositoriesLock)
    {
        EnsureLockFileExists();

        var configText = JsonConvert.SerializeObject(localRepositoriesLock);

        File.WriteAllText(RepositoriesLockFile.FullName, configText);
    }
    public void EnsureLockFileExists()
    {
        if (!RepositoriesLockFile.Exists)
        {
            RepositoriesLockFile.Create().Close();

            var defaultConfigText = JsonConvert.SerializeObject(DefaultRepositoriesLock, Formatting.Indented);

            File.WriteAllText(RepositoriesLockFile.FullName, defaultConfigText);
        }
    }
}
