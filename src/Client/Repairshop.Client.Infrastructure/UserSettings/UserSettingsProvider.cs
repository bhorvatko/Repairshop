using Repairshop.Client.Common.Interfaces;
using System.IO;
using System.Reflection;
using System.Text.Json;

namespace Repairshop.Client.Infrastructure.UserSettings;

internal class UserSettingsProvider<TUserSettings> 
    : IUserSettingsProvider<TUserSettings>
    where TUserSettings : new()
{
    private TUserSettings? userSettings;

    public TUserSettings GetSettings()
    {
        if (userSettings is not null)
        {
            return userSettings;
        }

        string settingsFileName = GetSettingsFilename();

        if (!File.Exists(settingsFileName))
        {
            return userSettings = new TUserSettings();
        }

        string jsonText = File.ReadAllText(settingsFileName);

        return userSettings = 
            JsonSerializer.Deserialize<TUserSettings>(jsonText)!;
    }

    public void SaveSettings(TUserSettings userSettings)
    {
        string jsonString = JsonSerializer.Serialize(userSettings);

        File.WriteAllText(GetSettingsFilename(), jsonString);

        this.userSettings = userSettings;
    }

    private static string GetSettingsFilename()
    {
        string directory = 
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        
        return Path.Combine(directory, typeof(TUserSettings).Name + ".json");
    }
}
