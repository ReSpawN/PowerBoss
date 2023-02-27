namespace PowerBoss.Domain.Exceptions;

public class TeslaHttpException : HttpRequestException
{
    public TeslaHttpException(string? message) : base(message)
    {
    }
}