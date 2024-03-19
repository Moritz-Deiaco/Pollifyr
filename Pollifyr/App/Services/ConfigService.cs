using Lephu_Umfrage.App.Config;
using Lephu_Umfrage.App.Helpers;

using Newtonsoft.Json;


namespace Lephu_Umfrage.App.Services;

public class ConfigService
{
    private readonly string Path = PathBuilder.File("storage", "config.json");
    private ConfigModel Data;
    
    public ConfigService()
    {
        Reload();
    }
    
    public void Reload()
    {
        if(!File.Exists(Path))
            File.WriteAllText(Path, "{}");

        var text = File.ReadAllText(Path);
        Data = JsonConvert.DeserializeObject<ConfigModel>(text) ?? new();
        Save();
    }

    public void Save()
    {
        var text = JsonConvert.SerializeObject(Data, Formatting.Indented);
        File.WriteAllText(Path, text);
    }

    public ConfigModel Get()
    {
        return Data;
    }
}