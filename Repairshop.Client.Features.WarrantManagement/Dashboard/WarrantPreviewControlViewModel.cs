using CommunityToolkit.Mvvm.ComponentModel;

namespace Repairshop.Client.Features.WarrantManagement.Dashboard;
public class WarrantPreviewControlViewModel
    : ObservableObject
{
    private string _labelContent;

    public WarrantViewModel Warrant { get; set; }
    public string LabelContent { get => _labelContent; set => SetProperty(ref _labelContent, value); }
    public bool PlayUpdateAnimation { get; set; }

    public WarrantPreviewControlViewModel(WarrantViewModel warrant)
    {
        Warrant = warrant;

        _labelContent = warrant.Title;
    }
}
