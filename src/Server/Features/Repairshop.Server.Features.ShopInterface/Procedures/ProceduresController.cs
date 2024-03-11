using Microsoft.AspNetCore.Mvc;
using Repairshop.Server.Common;
using Repairshop.Shared.Features.ShopInterface.Procedures;

namespace Repairshop.Server.Features.ShopInterface.Procedures;

public class ProceduresController
    : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateProcedure(
        CreateProcedureRequest request, 
        CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request, cancellationToken);

        return Ok(response);
    }
}
