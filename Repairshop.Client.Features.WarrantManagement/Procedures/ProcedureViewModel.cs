using Repairshop.Client.Common.Extensions;
using System.Windows;
using System.Windows.Media;

namespace Repairshop.Client.Features.WarrantManagement.Procedures;

public class ProcedureViewModel
    : ProcedureSummaryViewModel
{
    protected ProcedureViewModel(
        string name, 
        Color backgroundColor,
        Guid id,
        float priority,
        IEnumerable<string>? usedByWarrants = null,
        IEnumerable<string>? usedByWarrantTemplates = null) 
        : base(name, backgroundColor, id, priority)
    {
        UsedByWarrants = usedByWarrants ?? Enumerable.Empty<string>();
        UsedByWarrantTemplates = usedByWarrantTemplates ?? Enumerable.Empty<string>();
    }

    public IEnumerable<string> UsedByWarrants { get; private set; }
    public IEnumerable<string> UsedByWarrantTemplates { get; private set; }

    public bool CanBeDeleted =>
        !UsedByWarrants.Any() && !UsedByWarrantTemplates.Any();
    
    public string CannotBeDeletedMessage =>
        (UsedByWarrants.Any() 
            ? CannotBeDeletedBecauseOfWarrantsMessage 
            : string.Empty) +
        Environment.NewLine +
        Environment.NewLine +
        (UsedByWarrantTemplates.Any() 
            ? CannotBeDeletedBecauseOfWarrantTemplatesMessage 
            : string.Empty);

    public string CannotBeDeletedBecauseOfWarrantTemplatesMessage =>
        "Procedura ne može biti obrisana jer se koristi kod predložaka naloga: " +
            Environment.NewLine +
            string.Join(Environment.NewLine, UsedByWarrantTemplates.Select(x => "- " + x));

    public string CannotBeDeletedBecauseOfWarrantsMessage =>
        "Procedura ne može biti obrisana jer se koristi kod naloga: " +
            Environment.NewLine +
            string.Join(Environment.NewLine, UsedByWarrants.Select(x => "- " + x));

    public Visibility CannotBeDeletedTooltipVisibility =>
        (!CanBeDeleted).ToVisibility();

    public static ProcedureViewModel Create(
        Guid id,
        string name,
        string color,
        float priority,
        IEnumerable<string> usedByWarrants,
        IEnumerable<string> usedByWarrantTemplates)
    {
        return new ProcedureViewModel(
            name,
            (Color)ColorConverter.ConvertFromString("#" + color),
            id,
            priority,
            usedByWarrants,
            usedByWarrantTemplates);
    }
}
