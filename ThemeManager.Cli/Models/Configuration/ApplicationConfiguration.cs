using Newtonsoft.Json;

namespace ThemeManager.Cli.Models.Configuration;
public sealed class ApplicationConfiguration
{
    public string? CurrentTheme { get; set; }
    public Dictionary<string, string> ThemeRepositories { get; set; } = new();
    public IEnumerable<string> CustomThemes { get; set; } = Enumerable.Empty<string>();

    public static ApplicationConfiguration FromFile(FileInfo fileInfo)
    {
        return JsonConvert
            .DeserializeObject<ApplicationConfiguration>(
                File.ReadAllText(fileInfo.FullName))!;
    }
}
