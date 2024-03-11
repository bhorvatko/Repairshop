using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Repairshop.Server.Common.Entities;
using Repairshop.Server.Common.Events;

namespace Repairshop.Server.Infrastructure.Persistence;

public class EventPublishingInterceptor
     : SaveChangesInterceptor
{
    private readonly IMediator _mediator;

    public EventPublishingInterceptor(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData, 
        int result, 
        CancellationToken cancellationToken = default)
    {
        IEnumerable<EntityBase> entityEntries = 
            eventData.Context?.ChangeTracker.Entries<EntityBase>().Select(x => x.Entity)
                ?? Enumerable.Empty<EntityBase>();

        foreach (EntityBase entry in entityEntries)
        {
            foreach (DomainEvent domainEvent in entry.DomainEvents)
            {
                await _mediator.Publish(domainEvent, cancellationToken);
            }
        }

        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }
}
