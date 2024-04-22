using Microsoft.AspNetCore.Mvc;
using Repairshop.Server.Common.Controllers;
using Repairshop.Shared.Features.WarrantManagement.WarrantTemplates;

namespace Repairshop.Server.Features.WarrantManagement.WarrantTemplates;

public class WarrantTemplateController : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateWarrantTemplate(
        [FromBody] CreateWarrantTemplateRequest request,
        CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request, cancellationToken);

        return Ok(response);
    }
}
