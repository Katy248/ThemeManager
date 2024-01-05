using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ThemeManager.Cli.Models;

namespace ThemeManager.Cli.Managers;
public class DownloadThemeManager
{
    public static readonly string ThemesFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".themes");
    private readonly FileSystemManager _fileSystemManager;
    private readonly LocalThemeManager _localThemeManager;

    public DownloadThemeManager(FileSystemManager fileSystemManager, LocalThemeManager localThemeManager)
    {
        _fileSystemManager = fileSystemManager;
        _localThemeManager = localThemeManager;
    }
    public void DownloadTheme(ThemeConfig theme)
    {
        var themeDirectory = Path.Combine(ThemesFolder, theme.Name);
        _fileSystemManager.EnsureDelete(themeDirectory);
        Directory.CreateDirectory(themeDirectory);

        using var powerShell = PowerShell.Create();
        foreach (var appTheme in theme.ApplicationThemes)
        {
            var appThemeDirectory = Path.Combine(themeDirectory, appTheme.Key);
            Directory.CreateDirectory(appThemeDirectory);
            powerShell.AddScript($"git clone {appTheme.Value} {appThemeDirectory}");
            powerShell.Invoke();
        }
    }
    public void DeleteTheme(string themeName)
    {
        if (_localThemeManager.ThemeExists(themeName))
        {
            _fileSystemManager.EnsureDelete(Path.Combine(ThemesFolder, themeName));
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
