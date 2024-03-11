using Ardalis.Specification;

namespace Repairshop.Server.Common.Persistence;

public interface IRepository<T>
    : IRepositoryBase<T>
    where T : class
{
}
