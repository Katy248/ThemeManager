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

    private readonly DownloadThemeManager _downloadThemeManager;
    private readonly LocalRepositoryManager _localRepositoryManager;

    public ThemeManager(DownloadThemeManager downloadThemeManager, LocalRepositoryManager localRepositoryManager)
    {
        _downloadThemeManager = downloadThemeManager;
        _localRepositoryManager = localRepositoryManager;
    }
    public void SetTheme(string name, string? repository = null)
    {
        if (!string.IsNullOrWhiteSpace(repository))
        {
            var repo = 
        }
        var themes = _localRepositoryManager.GetLocalRepositories();

        if (!themes.Contains(name))
            return;

        CurrentThemeDirectory.EnsureEmpty();

        var themeDirectory = new DirectoryInfo(Path.Combine(ThemesFolder, name));

        themeDirectory.CopyTo(CurrentThemeDirectory.FullName);
    }
}
