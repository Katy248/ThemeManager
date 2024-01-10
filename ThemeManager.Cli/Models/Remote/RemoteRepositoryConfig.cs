using Newtonsoft.Json;

namespace ThemeManager.Cli.Models.Remote;
public class RemoteRepositoryConfig
{
    public static readonly RemoteRepositoryConfig Default = new();

    public IEnumerable<RemoteTheme> Themes { get; set; } = Enumerable.Empty<RemoteTheme>();
    
    public static RemoteRepositoryConfig FromFile(FileInfo fileInfo)
    {
        return JsonConvert
            .DeserializeObject<RemoteRepositoryConfig>(File.ReadAllText(fileInfo.FullName)) ?? Default;
    }
}
