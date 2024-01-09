using Newtonsoft.Json;
using ThemeManager.Cli.Models.Configuration;

namespace ThemeManager.Cli.Managers;
public class ConfigManager
{
    public static readonly string ConfigFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".config/ThemeManager/config.json");
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

    private ApplicationConfiguration GetConfig()
    {
        EnsureConfigurationFileExists();

        var config = JsonConvert.DeserializeObject<ApplicationConfiguration>(File.ReadAllText(ConfigFilePath));

        return config;
    }

    private void SaveConfig(ApplicationConfiguration config)
    {
        var configText = JsonConvert.SerializeObject(config);

        File.WriteAllText(ConfigFilePath, configText);
    }

    private void EnsureConfigurationFileExists()
    {
        if (!File.Exists(ConfigFilePath))
        {
            var config = JsonConvert.SerializeObject(new ApplicationConfiguration());
            Directory.CreateDirectory(Directory.GetParent(ConfigFilePath)?.FullName ?? "");
            File.Create(ConfigFilePath).Close();
            File.WriteAllText(ConfigFilePath, config);
        }
    }
}
