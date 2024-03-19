using System.Windows;
using System.Windows.Controls;

namespace Repairshop.Client.Infrastructure.Navigation;

public abstract class MainView
    : Window
{
    public abstract ContentControl MainContentControl { get; }
}
