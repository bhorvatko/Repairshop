using Repairshop.Server.Features.WarrantManagement.Warrants;

namespace Repairshop.Server.Tests.Shared.Features.WarrantManagement;
public static class WarrantHelper
{
    public async static Task<Warrant> Create(
        string title = "Title",
        DateTime? deadline = null,
        bool isUrgent = false,
        IEnumerable<WarrantStep>? steps = null) =>
        Warrant.Create(
            title,
            deadline ?? DateTime.MaxValue,
            isUrgent,
            steps ?? await WarrantStepHelper.CreateStepSequence(3));
}
