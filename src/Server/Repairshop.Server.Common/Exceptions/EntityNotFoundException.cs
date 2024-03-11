namespace Repairshop.Server.Common.Exceptions;

public class EntityNotFoundException<TEntity, TId>
    : EntityNotFoundException
{
    public EntityNotFoundException(TId id)
        : base($"Entity of type {typeof(TEntity).Name} with ID {id} not found")
    {
    }
}

public class EntityNotFoundException
    : Exception
{
    public EntityNotFoundException(string message) 
        : base(message) 
    { 
    }
}
