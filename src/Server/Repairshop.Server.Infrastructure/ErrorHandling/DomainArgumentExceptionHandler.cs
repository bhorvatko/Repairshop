using Microsoft.AspNetCore.Http;
using Repairshop.Server.Common.Exceptions;

namespace Repairshop.Server.Infrastructure.ErrorHandling;

internal class DomainArgumentExceptionHandler
    : ExceptionHandlerBase<DomainArgumentException>
{
    protected override int StatusCode => StatusCodes.Status400BadRequest;

    protected override string Title => "Bad Request";
}
