using System.Windows;

namespace Repairshop.Client.Common.Extensions;
public static class BoolExtensions
{
    public static Visibility ToVisibility(this bool b) =>
        b ? Visibility.Visible : Visibility.Collapsed;
}
