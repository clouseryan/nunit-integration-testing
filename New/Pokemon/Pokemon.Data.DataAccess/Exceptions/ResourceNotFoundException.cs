namespace Pokemon.Data.DataAccess.Exceptions;

public class ResourceNotFoundException : Exception
{
    public string Identifier { get; private set; }
    public string Resource { get; private set; }
    public ResourceNotFoundException(string? message, Exception? innerException, string? identifier, string? resource) 
        : base(message, innerException)
    {
        Identifier = identifier ?? String.Empty;
        Resource = resource ?? String.Empty;
    }
}