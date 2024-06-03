using Repairshop.Server.Features.WarrantManagement.Data;
using Repairshop.Server.Features.WarrantManagement.Warrants;

namespace Repairshop.Server.Tests.Shared.Features.WarrantManagement;
public static class WarrantHelper
{
    public async static Task<Warrant> Create(
        string title = "Title",
        DateTime? deadline = null,
        bool isUrgent = false,
        IEnumerable<WarrantStep>? steps = null,
        Func<DateTimeOffset>? getUtcNow = null) =>
        await Create(
            null,
            title,
            deadline,
            isUrgent,
            steps,
            getUtcNow);

    public async static Task<Warrant> CreateAndAddWarrantToDbContext(
        WarrantManagementDbContext dbContext,
        string title = "Title",
        DateTime? deadline = null,
        bool isUrgent = false,
        IEnumerable<WarrantStep>? steps = null,
        Func<DateTimeOffset>? getUtcNow = null)
    {
        Warrant warrant = await Create(
            dbContext,
            title,
            deadline,
            isUrgent,
            steps,
            getUtcNow);

        await dbContext.SaveChangesAsync();

        return warrant;
    }

    private async static Task<Warrant> Create(
        WarrantManagementDbContext? dbContext = null,
        string title = "Title",
        DateTime? deadline = null,
        bool isUrgent = false,
        IEnumerable<WarrantStep>? steps = null,
        Func<DateTimeOffset>? getUtcNow = null) =>
        await Warrant.Create(
            title,
            deadline ?? new DateTime(2000, 1, 1),
            isUrgent,
            1,
            steps ?? await WarrantStepHelper.CreateStepSequence(3),
            getUtcNow ?? (() => DateTimeOffset.UtcNow),
            dbContext is null 
                ? warrant => Task.CompletedTask
                : async warrant =>
                {
                    await dbContext.Set<Warrant>().AddAsync(warrant);
                    await dbContext.SaveChangesAsync();
                });

}
