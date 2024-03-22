namespace Repairshop.Server.Infrastructure.ErrorHandling;

internal class BadRequestException
    : Exception
{
    public BadRequestException(string message)
        : base(message)
    {
        
    }
}
