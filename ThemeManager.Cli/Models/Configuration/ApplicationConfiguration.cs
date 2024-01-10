using Newtonsoft.Json;
using ThemeManager.Cli.FileSystem;

namespace ThemeManager.Cli.Models.Configuration;
public sealed class ApplicationConfiguration
{
    private FileInfo _fileInfo;

    public ApplicationConfiguration() {}
    public string? CurrentTheme { get; set; }
    public Dictionary<string, string> ThemeRepositories { get; set; } = new();
    public IEnumerable<string> CustomThemes { get; set; } = Enumerable.Empty<string>();

    public void Save()
    {
        if (_fileInfo is null)
            return;

        _fileInfo.EnsureCreated();

        var configText = JsonConvert.SerializeObject(this, Formatting.Indented);

        File.WriteAllText(_fileInfo.FullName, configText);
    }

    public static ApplicationConfiguration FromFile(FileInfo fileInfo)
    {
        var config = JsonConvert
            .DeserializeObject<ApplicationConfiguration>(
                File.ReadAllText(fileInfo.FullName))!;
        config._fileInfo = fileInfo;

        return config;
    }

}
