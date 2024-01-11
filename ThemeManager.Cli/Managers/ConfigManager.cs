using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ThemeManager.Cli.Models.Configuration;

namespace ThemeManager.Cli.Managers;
public class ConfigManager
{
    public static readonly FileInfo ConfigFile = new(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".config/ThemeManager/config.json"));
    private readonly ILogger<ConfigManager> _logger;
    private readonly LocalRepositoryManager _localRepositoryManager;
    private readonly ThemeManager _themeManager;
    private readonly ApplicationConfiguration _config;

    public ConfigManager(ILogger<ConfigManager> logger, LocalRepositoryManager localRepositoryManager, ThemeManager themeManager)
    {
        _logger = logger;
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
        else
        {
            _logger.LogWarning($"ThemeRepositories already contains repository with url '{repositoryUrl}'");
        }
    }
    public void RemoveRepository(string repositoryUrl)
    {
        _logger.LogDebug($"Start removing repository with url '{repositoryUrl}'");

        _localRepositoryManager.RemoveRepository(repositoryUrl);
        _config.ThemeRepositories.Remove(repositoryUrl);

        _config.Save();
        _logger.LogDebug($"Finnish removing repository with url '{repositoryUrl}'");
    }

    public void SetTheme(string themeName, string? repository) 
    {
        _themeManager.SetTheme(themeName, _config.ThemeRepositories, repository);

        _config.CurrentTheme = $"{themeName}{(string.IsNullOrWhiteSpace(repository) ? $"@{repository}" : string.Empty)}";
        _config.Save();
    }
    public void Reload()
    {
        _logger.LogDebug($"Start reloading configuration");

        _logger.LogDebug($"Removing all repositories");
        _localRepositoryManager.RemoveAllRepositories();

        _logger.LogDebug($"Add repositories from configuration");
        foreach (var repository in _config.ThemeRepositories)
        {
            _localRepositoryManager.AddRepository(repository.Value);
        }

        var theme = _config.CurrentTheme?.Split("@").FirstOrDefault();
        var themeRepository = _config.CurrentTheme?.Split("@").LastOrDefault(); 

        if (theme is not null)
        {
            _logger.LogDebug($"Set theme to '{theme}' of repository '{(themeRepository ?? "NO_REPOSITORY_SPECIFIED")}'");
            _themeManager.SetTheme(theme, _config.ThemeRepositories, themeRepository);
        }
        else
        {
            _logger.LogDebug("Current theme not found");
        }
    }

    public ApplicationConfiguration GetApplicationConfiguration()
    {
        return _config;
    }
}
