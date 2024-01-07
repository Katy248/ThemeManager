using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ThemeManager.Cli.FileSystem;
using ThemeManager.Cli.Models;

namespace ThemeManager.Cli.Managers;
public class DownloadThemeManager
{
    public static readonly string ThemesFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".themes");
    private readonly LocalThemeManager _localThemeManager;

    public DownloadThemeManager(LocalThemeManager localThemeManager)
    {
        _localThemeManager = localThemeManager;
    }
    public void DownloadTheme(ThemeConfig theme)
    {
        var themeDirectory = new DirectoryInfo(Path.Combine(ThemesFolder, theme.Name)).EnsureEmpty();
        
        using var powerShell = PowerShell.Create();
        foreach (var appTheme in theme.ApplicationThemes)
        {
            var appThemeDirectory = new DirectoryInfo(Path.Combine(themeDirectory.FullName, appTheme.Key)).EnsureCreated();

            powerShell.AddScript($"git clone {appTheme.Value} {appThemeDirectory}");
            powerShell.Invoke();
        }
    }
    public void DeleteTheme(string themeName)
    {
        if (_localThemeManager.ThemeExists(themeName))
        {
            new DirectoryInfo(Path.Combine(ThemesFolder, themeName)).EnsureDeleted();
        }
    }
    public IEnumerable<string> GetDownloadedThemes()
    {
        var directories = Directory.GetDirectories(ThemesFolder);

        var localThemes = _localThemeManager.GetThemes().Select(theme => theme.Name);

        foreach (var directory in directories)
        {
            var dirName = new DirectoryInfo(directory).Name.Trim();
            if (localThemes.Contains(dirName))
            {
                yield return dirName;
            }
        }
    }
}
