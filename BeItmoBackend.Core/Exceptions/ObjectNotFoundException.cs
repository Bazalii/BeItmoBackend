namespace BeItmoBackend.Core.Exceptions;

public class ObjectNotFoundException : Exception
{
    public ObjectNotFoundException(string message)
        : base(message)
    {
    }
}