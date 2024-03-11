using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Repairshop.Server.Infrastructure.ErrorHandling;

internal abstract class ExceptionHandlerBase<TException>
    : IExceptionHandler
    where TException : Exception
{
    protected abstract int StatusCode { get; }
    protected abstract string Title { get; }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not TException)
        {
            return false;
        }

        ProblemDetails problemDetails = new ProblemDetails()
        {
            Status = StatusCode,
            Title = Title,
            Detail = exception.Message
        };

        httpContext.Response.StatusCode =
            problemDetails.Status.Value;

        await httpContext
            .Response
            .WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
