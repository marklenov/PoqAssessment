namespace PoqAssessment.Exceptions;

public class AppException : ApplicationException
{
    public Dictionary<string, string[]> Errors { get; }

    public AppException(string message) : base(message)
    {
        Errors = new();
    }

    public AppException() : base("App exception have occured.")
    {
        Errors = new();
    }
}
