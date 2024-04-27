using Repairshop.Client.Infrastructure.Bootstrapping;
using Repairshop.Shared.Common.ClientContext;

namespace Repairshop.Client.FrontDesk;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : AppBase<MainWindow, MainViewModel>
{
    public App()
        : base(RepairshopClientContext.FrontOffice)
    {
    }
}

