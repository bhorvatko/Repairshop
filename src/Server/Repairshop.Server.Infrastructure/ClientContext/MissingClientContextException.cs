using Repairshop.Server.Infrastructure.ErrorHandling;
using Repairshop.Shared.Common.ClientContext;

namespace Repairshop.Server.Infrastructure.ClientContext;

internal class MissingClientContextException
    : BadRequestException
{
    public MissingClientContextException()
        : base($"{ClientContextConstants.ClientContextHeader} header is missing from the request.")
    {
        
    }
}
