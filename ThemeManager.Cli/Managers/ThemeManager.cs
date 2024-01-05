using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace ThemeManager.Cli.Managers;
public class ThemeManager
{
    public static readonly string CurrentThemeFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".themes/current");
    public static readonly string ThemesFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".themes");

    private readonly LocalThemeManager _localThemeManager;
    private readonly DownloadThemeManager _downloadThemeManager;
    private readonly FileSystemManager _fileSystemManager;

    public ThemeManager(LocalThemeManager localThemeManager, DownloadThemeManager downloadThemeManager, FileSystemManager fileSystemManager)
    {
        _localThemeManager = localThemeManager;
        _downloadThemeManager = downloadThemeManager;
        _fileSystemManager = fileSystemManager;
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

        _fileSystemManager.EnsureDelete(CurrentThemeFolder);
        Directory.CreateDirectory(CurrentThemeFolder);

        _fileSystemManager.CopyFilesRecursively(Path.Combine(ThemesFolder, name), CurrentThemeFolder);
    }
    public void RemoveTheme(string name)
    {
        _downloadThemeManager.DeleteTheme(name);
    }
    
}
