﻿using Microsoft.AspNetCore.Mvc;
using Repairshop.Server.Common.Controllers;
using Repairshop.Shared.Features.WarrantManagement.Warrants;

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

    [HttpGet]
    public async Task<IActionResult> GetWarrants(
        [FromQuery] GetWarrantsRequest request,
        CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request, cancellationToken);

        return Ok(response);
    }
}