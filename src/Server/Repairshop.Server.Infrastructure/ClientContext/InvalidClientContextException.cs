using Repairshop.Server.Infrastructure.ErrorHandling;

namespace Repairshop.Server.Infrastructure.ClientContext;

internal class InvalidClientContextException
    : BadRequestException
{
    public InvalidClientContextException() 
        : base("Client context header contains invaid value.")
    {
    }
}
