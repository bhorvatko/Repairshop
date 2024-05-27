using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Media;

namespace Repairshop.Client.Features.WarrantManagement.Procedures;

public class ProcedureSummaryViewModel
    : ObservableObject
{
    private Color _backgroundColor;
    private string _name;

    protected ProcedureSummaryViewModel(
        string name,
        Color backgroundColor,
        Guid id,
        float priority)
    {
        _name = name;
        _backgroundColor = backgroundColor;
        Id = id;
        Priority = priority;
    }

    public Guid Id { get; private set; }
    public string Name { get => _name; set => SetProperty(ref _name, value); }
    public float Priority { get; private set; }

    public Color BackgroundColor
    { 
        get => _backgroundColor;
        set
        {
            SetProperty(ref _backgroundColor, value);

            OnPropertyChanged(nameof(BackgroundColorBrush));
            OnPropertyChanged(nameof(ForegroundColorBrush));
        }
    }

    public Color ForegroundColor => GetForegroundColor();
    public Brush BackgroundColorBrush => ConvertToBrush(BackgroundColor);
    public Brush ForegroundColorBrush => ConvertToBrush(GetForegroundColor());

    public static ProcedureSummaryViewModel Create(
        Guid id,
        string name,
        string color,
        float priority)
    {
        return new ProcedureSummaryViewModel(
            name, 
            (Color)ColorConverter.ConvertFromString("#" + color),
            id,
            priority);
    }

    public void SetPriority(float priority) =>
        Priority = priority;

    private Color GetForegroundColor() =>
        new[] { (int)BackgroundColor.R, (int)BackgroundColor.G, (int)BackgroundColor.B }.Average() > 255 / 2
            ? Color.FromRgb(0, 0, 0)
            : Color.FromRgb(255, 255, 255);


    private static Brush ConvertToBrush(Color color) =>
        new SolidColorBrush(color);
}
