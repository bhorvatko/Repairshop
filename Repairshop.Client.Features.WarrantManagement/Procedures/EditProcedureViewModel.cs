using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace Repairshop.Client.Features.WarrantManagement.Procedures;

public partial class EditProcedureViewModel
    : ObservableValidator
{
    [ObservableProperty]
    [Required(ErrorMessage = "Unesite ime procedure")]
    private string _name = string.Empty;

    [ObservableProperty]
    private System.Windows.Media.Color _backgroundColor =
        System.Windows.Media.Color.FromArgb(
            255,
            (byte)Random.Shared.Next(255),
            (byte)Random.Shared.Next(255),
            (byte)Random.Shared.Next(255));

    public bool Validate()
    {
        ValidateAllProperties();

        return !HasErrors;
    }

    public string GetColorAsRgb() => ColorToRgb(BackgroundColor);

    private static string ColorToRgb(System.Windows.Media.Color color) =>
        ColorTranslator.ToHtml(
            Color.FromArgb(color.R, color.G, color.B))
                .Replace("#", "");
}
