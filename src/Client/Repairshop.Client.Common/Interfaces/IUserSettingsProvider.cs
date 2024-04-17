namespace Repairshop.Client.Common.Interfaces;

public interface IUserSettingsProvider<TUserSettings> 
    where TUserSettings : new()
{
    TUserSettings GetSettings();

    void SaveSettings(TUserSettings userSettings);
}
