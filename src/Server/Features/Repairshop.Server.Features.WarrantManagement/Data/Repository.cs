using Ardalis.Specification.EntityFrameworkCore;
using Repairshop.Server.Common.Persistence;

namespace Repairshop.Server.Features.WarrantManagement.Data;

internal class Repository<T>
    : RepositoryBase<T>, IRepository<T>
    where T : class
{
    public Repository(WarrantManagementDbContext dbContext) 
        : base(dbContext)
    {
    }
}
