using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ThemeManager.Cli.Models;

namespace ThemeManager.Cli.Managers;
public class LocalThemeManager
{
    public static readonly string ThemesFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".themes");
    const string CurrentThemeFolderName = "current"; 

    private readonly ConfigManager _configManager;
    private readonly RepositoryManager _repositoryManager;

    public LocalThemeManager(ConfigManager configManager, RepositoryManager repositoryManager)
    {
        _configManager = configManager;
        _repositoryManager = repositoryManager;
    }
    public bool ThemeExists(string name)
    {
        var themes = GetThemes();

        return themes.Any(theme => theme.Name == name);
    }
    public ThemeConfig? GetThemeConfig(string themeName)
    {
        var localThemes = GetThemes();
        
        var theme = localThemes.FirstOrDefault(lt => lt.Name == themeName);

        return GetThemeConfig(theme);
    }
    public ThemeConfig? GetThemeConfig(LocalTheme? theme)
    {
        if (theme is null) 
            return null;

        var configText = File.ReadAllText(theme.FullPath);
        var config = JsonConvert.DeserializeObject<ThemeConfig>(configText);

        return config;
    }
    public IEnumerable<LocalTheme> GetThemes()
    {
        var repositoryThemes = _repositoryManager
            .GetRepositories()
            .SelectMany(repo => repo.Themes);
        
        // TODO: Add custom themes support
        
        return repositoryThemes;
    }
}
