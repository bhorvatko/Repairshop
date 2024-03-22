using Microsoft.AspNetCore.Http;

namespace Repairshop.Server.Infrastructure.ErrorHandling;

internal class BadRequestExceptionHandler
    : ExceptionHandlerBase<BadRequestException>
{
    protected override int StatusCode => StatusCodes.Status400BadRequest;

    protected override string Title => "Bad request";
}
