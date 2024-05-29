﻿using MediatR;

namespace Repairshop.Shared.Features.WarrantManagement.Warrants;

public class CreateWarrantRequest
    : IRequest<CreateWarrantResponse>
{
    public required string Title { get; set; }
    public required DateTime Deadline { get; set; }
    public required bool IsUrgnet { get; set; }
    public required int Number { get; set; }
    public required IEnumerable<WarrantStepDto> Steps { get; set; }
}
