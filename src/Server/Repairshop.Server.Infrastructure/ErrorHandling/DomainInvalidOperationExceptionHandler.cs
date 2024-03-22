﻿using Microsoft.AspNetCore.Http;
using Repairshop.Server.Common.Exceptions;

namespace Repairshop.Server.Infrastructure.ErrorHandling;
internal class DomainInvalidOperationExceptionHandler
    : ExceptionHandlerBase<DomainInvalidOperationException>
{
    protected override int StatusCode => StatusCodes.Status422UnprocessableEntity;

    protected override string Title => "Unprocessable entity";
}