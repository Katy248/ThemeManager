using Newtonsoft.Json;
using ThemeManager.Cli.Models.Configuration;

namespace ThemeManager.Cli.Managers;
public class ConfigManager
{
    public static readonly FileInfo ConfigFile = new(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".config/ThemeManager/config.json"));
    private readonly LocalRepositoryManager _localRepositoryManager;
    private readonly ThemeManager _themeManager;
    private readonly ApplicationConfiguration _config;

    public ConfigManager(LocalRepositoryManager localRepositoryManager, ThemeManager themeManager)
    {
        _localRepositoryManager = localRepositoryManager;
        _themeManager = themeManager;
        _config = ApplicationConfiguration.FromFile(ConfigFile);
    }
    public void AddRepository(string repositoryUrl, string? repositoryName = null)
    {
        repositoryName ??= repositoryUrl;

        _localRepositoryManager.AddRepository(repositoryUrl);


        if (!_config.ThemeRepositories.Any(repo => repo.Value == repositoryUrl))
        {
            _config.ThemeRepositories.Add(repositoryName, repositoryUrl);
            _config.Save();
        }
    }
    public void RemoveRepository(string repositoryUrl)
    {
        _config.ThemeRepositories.Remove(repositoryUrl);

        _config.Save();
    }

    public void SetTheme(string themeName, string? repository) 
    {
        _themeManager.SetTheme(themeName, _config.ThemeRepositories, repository);

        _config.CurrentTheme = $"{themeName}{(string.IsNullOrWhiteSpace(repository) ? $"@{repository}" : string.Empty)}";
    }
    public void Reload()
    {
        _localRepositoryManager.RemoveAllRepositories();

        foreach (var repository in _config.ThemeRepositories)
        {
            _localRepositoryManager.AddRepository(repository.Key);
        }

        var theme = _config.CurrentTheme?.Split("@").FirstOrDefault();
        var themeRepository = _config.CurrentTheme?.Split("@").LastOrDefault(); 

        if (theme is not null)
        {
            _themeManager.SetTheme(theme, _config.ThemeRepositories, themeRepository);
        }
    }

    public ApplicationConfiguration GetApplicationConfiguration()
    {
        return _config;
    }
}
