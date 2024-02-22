using Logging.Net;

namespace Lephu_Umfrage.App.Helpers;

public class ConfigHelper
{
    public ConfigHelper()
    {
        
    }

    public Task Perform()
    {
        Logger.Info("Checking config file");
        var path = PathBuilder.File("storage", "config.json");

        if (!Directory.Exists(PathBuilder.Dir("storage")))
        {
            Directory.CreateDirectory(PathBuilder.Dir("storage"));
        }
        
        if (File.Exists(path))
        {
            Logger.Info("Config file exists, continuing startup");
            return Task.CompletedTask;
        }

        FileStream fs = File.Create(path);
        fs.Close();
        return Task.CompletedTask;
    }
}