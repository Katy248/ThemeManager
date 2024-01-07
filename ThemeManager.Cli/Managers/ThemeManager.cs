using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using ThemeManager.Cli.FileSystem;

namespace ThemeManager.Cli.Managers;
public class ThemeManager
{
    public static readonly DirectoryInfo CurrentThemeDirectory = new(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".themes/current"));
    public static readonly string ThemesFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".themes");

    private readonly LocalThemeManager _localThemeManager;
    private readonly DownloadThemeManager _downloadThemeManager;

    public ThemeManager(LocalThemeManager localThemeManager, DownloadThemeManager downloadThemeManager)
    {
        _localThemeManager = localThemeManager;
        _downloadThemeManager = downloadThemeManager;
    }
    public void AddTheme(string name)
    {
        var theme = _localThemeManager.GetThemeConfig(name);

        _downloadThemeManager.DownloadTheme(theme);
    }

    public void SetTheme(string name)
    {
        var themes = _downloadThemeManager.GetDownloadedThemes();

        if (!themes.Contains(name))
            return;

        CurrentThemeDirectory.EnsureEmpty();

        var themeDirectory = new DirectoryInfo(Path.Combine(ThemesFolder, name));

        themeDirectory.CopyTo(CurrentThemeDirectory.FullName);
    }
    public void RemoveTheme(string name)
    {
        _downloadThemeManager.DeleteTheme(name);
    }
    
}
