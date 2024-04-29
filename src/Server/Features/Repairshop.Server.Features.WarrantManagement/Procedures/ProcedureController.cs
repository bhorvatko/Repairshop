using Microsoft.AspNetCore.Mvc;
using Repairshop.Server.Common.Controllers;
using Repairshop.Shared.Features.WarrantManagement.Procedures;

namespace Repairshop.Server.Features.WarrantManagement.Procedures;

public class ProcedureController
    : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateProcedure(
        [FromBody] CreateProcedureRequest request,
        CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request, cancellationToken);

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetProcedures(
        [FromQuery] GetProceduresRequest request,
        CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request, cancellationToken);

        return Ok(response);
    }

    [HttpGet]
    [Route("Summaries")]
    public async Task<IActionResult> GetProcedreSummaries(
        [FromQuery] GetProcedureSummariesRequest request,
        CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request, cancellationToken);

        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProcedure(
        [FromBody] UpdateProcedureRequest request,
        CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request, cancellationToken);

        return Ok(response);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteProcedure(
        [FromRoute] DeleteProcedureRequest request,
        CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request, cancellationToken);

        return Ok(response);
    }
}
