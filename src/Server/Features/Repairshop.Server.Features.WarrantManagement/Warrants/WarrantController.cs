﻿using Microsoft.AspNetCore.Mvc;
using Repairshop.Server.Common.Controllers;
using Repairshop.Shared.Features.WarrantManagement.Warrants;
using Repairshop.Shared.Features.WarrantManagement.Warrants.GetWarrantLog;

namespace Repairshop.Server.Features.WarrantManagement.Warrants;

public class WarrantController
    : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateWarrant(
        [FromBody] CreateWarrantRequest request,
        CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request, cancellationToken);

        return Ok(response);
    }

    [HttpPost("Advance")]
    public async Task<IActionResult> AdvanceWarrant(
        [FromBody] AdvanceWarrantRequest request,
        CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request, cancellationToken);

        return Ok(response);
    }

    [HttpPost("Rollback")]
    public async Task<IActionResult> RollbackWarrant(
        [FromBody] RollbackWarrantRequest request,
        CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request, cancellationToken);

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetWarrants(
        [FromQuery] GetWarrantsRequest request,
        CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request, cancellationToken);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetWarrant(
        [FromRoute] GetWarrantRequest request,
        CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request, cancellationToken);

        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateWarrant(
        [FromBody] UpdateWarrantRequest request,
        CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request, cancellationToken);

        return Ok(response);
    }

    [HttpPut]
    [Route("{id}/Unassign")]
    public async Task<IActionResult> UnassignWarrant(
        [FromRoute] UnassignWarrantRequest request,
        CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request, cancellationToken);

        return Ok(response);
    }

    [HttpGet]
    [Route("Log")]
    public async Task<IActionResult> GetWarrantLog(
        [FromQuery] GetWarrantLogRequest request,
        CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request, cancellationToken);

        return Ok(response);
    }
}
