namespace Repairshop.Server.Common.Exceptions;

public class DomainArgumentException
    : Exception
{
    public DomainArgumentException(
        object invalidArgument,
        string message)
        : base(message)
    {
        InvalidArgument = invalidArgument;
    }

    public object InvalidArgument { get; set; }
}
