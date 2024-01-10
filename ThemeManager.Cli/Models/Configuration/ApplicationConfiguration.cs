using Newtonsoft.Json;
using ThemeManager.Cli.FileSystem;

namespace ThemeManager.Cli.Models.Configuration;
public sealed class ApplicationConfiguration
{
    private readonly FileInfo _fileInfo;

    private ApplicationConfiguration(FileInfo fileInfo) 
    {
        _fileInfo = fileInfo;
        fileInfo.EnsureCreated();
    }
    public string? CurrentTheme { get; set; }
    public Dictionary<string, string> ThemeRepositories { get; set; } = new();
    public IEnumerable<string> CustomThemes { get; set; } = Enumerable.Empty<string>();

    public void Save()
    {
        _fileInfo.EnsureCreated();

        var configText = JsonConvert.SerializeObject(this);

        File.WriteAllText(_fileInfo.FullName, configText);
    }

    public static ApplicationConfiguration FromFile(FileInfo fileInfo)
    {
        return JsonConvert
            .DeserializeObject<ApplicationConfiguration>(
                File.ReadAllText(fileInfo.FullName))!;
    }

}
