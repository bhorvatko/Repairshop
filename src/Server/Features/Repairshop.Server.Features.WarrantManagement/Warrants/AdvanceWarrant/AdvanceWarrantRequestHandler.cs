﻿using MediatR;
using Repairshop.Server.Common.ClientContext;
using Repairshop.Server.Common.Exceptions;
using Repairshop.Server.Common.Persistence;
using Repairshop.Shared.Features.WarrantManagement.Warrants;

namespace Repairshop.Server.Features.WarrantManagement.Warrants.AdvanceWarrant;

internal class AdvanceWarrantRequestHandler
    : IRequestHandler<AdvanceWarrantRequest, AdvanceWarrantResponse>
{
    private readonly IRepository<Warrant> _warrants;
    private readonly IClientContextProvider _clientContextProvider;

    public AdvanceWarrantRequestHandler(
        IRepository<Warrant> warrants,
        IClientContextProvider clientContextProvider)
    {
        _warrants = warrants;
        _clientContextProvider = clientContextProvider;
    }

    public async Task<AdvanceWarrantResponse> Handle(
        AdvanceWarrantRequest request,
        CancellationToken cancellationToken)
    {
        GetWarrantSpecification query = new(request.WarrantId);

        Warrant? warrant =
            await _warrants.FirstOrDefaultAsync(query, cancellationToken);

        if (warrant is null)
        {
            throw new EntityNotFoundException<Warrant, Guid>(request.WarrantId);
        }

        warrant.AdvanceToStep(
            request.StepId, 
            _clientContextProvider.GetClientContext());

        await _warrants.SaveChangesAsync(cancellationToken);

        return new AdvanceWarrantResponse();
    }
}
