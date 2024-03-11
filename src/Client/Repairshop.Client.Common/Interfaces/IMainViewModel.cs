namespace Repairshop.Client.Common.Interfaces;

public interface IMainViewModel
{
    IViewModel? CurrentViewModel { get; set; }

    void ShowLoadingIndicator();
    void HideLoadingIndicator();
}
