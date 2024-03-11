namespace Repairshop.Server.Common.Exceptions;

public class DomainInvalidOperationException
    : InvalidOperationException
{
    public DomainInvalidOperationException(string message) 
        : base(message)
    { 
    }
}
