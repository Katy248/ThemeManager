﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.PowerShell.Commands;
using ThemeManager.Cli.FileSystem;

namespace ThemeManager.Cli.Managers;
public class ThemeManager
{
    public static readonly DirectoryInfo CurrentThemeDirectory = new(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".themes/current"));
    public static readonly string ThemesFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".themes");

    private readonly DownloadThemeManager _downloadThemeManager;
    private readonly LocalRepositoryManager _localRepositoryManager;
    private readonly ConfigManager _configManager;

    public ThemeManager(DownloadThemeManager downloadThemeManager, LocalRepositoryManager localRepositoryManager, ConfigManager configManager)
    {
        _downloadThemeManager = downloadThemeManager;
        _localRepositoryManager = localRepositoryManager;
        _configManager = configManager;
    }
    public void SetTheme(string name, string? repository = null)
    {
        var repositoriesToSearch = _localRepositoryManager.GetLocalRepositories();
        if (!string.IsNullOrWhiteSpace(repository))
        {
            var repositoryUrl = _configManager
                .GetApplicationConfiguration()
                .ThemeRepositories
                .FirstOrDefault(r => r.Key == repository && r.Value == repository)
                .Value;

            repositoriesToSearch = repositoriesToSearch.Where(r => r.RemoteUrl == repositoryUrl);

            if (!repositoriesToSearch.Any())
            {
                // TODO: replace exception
                throw new Exception($"There no repository for \"{repository}\"");
            }
        }

        var themeConfig = repositoriesToSearch
            .SelectMany(r => r.Themes)
            .FirstOrDefault(t => t.Name == name) ?? throw new Exception($"There no theme for \"{name}\"");

        CurrentThemeDirectory.EnsureEmpty();

        foreach (var appTheme in themeConfig.ApplicationThemes)
        {
            var appThemeDirectory = new DirectoryInfo(Path.Combine(CurrentThemeDirectory.FullName, appTheme.Application))
                .EnsureCreated();

            using var powerShell = PowerShell.Create();
            powerShell.AddScript($"git clone {appTheme.RemoteUrl} {appThemeDirectory.FullName}");
            powerShell.Invoke();
        }
    }
}
