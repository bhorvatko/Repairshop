using Repairshop.Server.Common.Events;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repairshop.Server.Common.Entities;

public abstract class EntityBase
{
    [NotMapped]
    public IEnumerable<DomainEvent> DomainEvents { get; private set; } = Enumerable.Empty<DomainEvent>();
    
    protected void AddEvent(DomainEvent domainEvent) =>
        DomainEvents = DomainEvents.Append(domainEvent);
}
