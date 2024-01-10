using Newtonsoft.Json;
using ThemeManager.Cli.Models.Configuration;

namespace ThemeManager.Cli.Managers;
public class ConfigManager
{
    public static readonly FileInfo ConfigFile = new(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".config/ThemeManager/config.json"));
    private readonly LocalRepositoryManager _localRepositoryManager;

    public ConfigManager(LocalRepositoryManager localRepositoryManager)
    {
        _localRepositoryManager = localRepositoryManager;
    }
    public void AddRepository(string repositoryUrl, string? repositoryName = null)
    {
        repositoryName ??= repositoryUrl;

        _localRepositoryManager.AddRepository(repositoryUrl);

        var config = GetConfig();

        if (!config.ThemeRepositories.Any(repo => repo.Value == repositoryUrl))
        {
            config.ThemeRepositories.Add(repositoryName, repositoryUrl);
            SaveConfig(config);
        }
    }
    public void RemoveRepository(string repositoryUrl)
    {
        var config = GetConfig();

        config.ThemeRepositories.Remove(repositoryUrl);

        SaveConfig(config);
    }

    public ApplicationConfiguration GetApplicationConfiguration()
    {
        return GetConfig();
    }
    public void EnsureConfigurationFileExists()
    {
        if (!ConfigFile.Exists)
        {
            var config = JsonConvert.SerializeObject(new ApplicationConfiguration());
            Directory.CreateDirectory(Directory.GetParent(ConfigFile.FullName)?.FullName ?? "");
            ConfigFile.Create().Close();
            File.WriteAllText(ConfigFile.FullName, config);
        }
    }

    private ApplicationConfiguration GetConfig()
    {
        EnsureConfigurationFileExists();

        var config = ApplicationConfiguration.FromFile(ConfigFile);

        return config;
    }

    private void SaveConfig(ApplicationConfiguration config)
    {
        var configText = JsonConvert.SerializeObject(config);

        File.WriteAllText(ConfigFile.FullName, configText);
    }

}
