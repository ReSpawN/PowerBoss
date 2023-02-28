namespace PowerBoss.Infra.Api.Tesla.Exceptions;

public class TeslaHttpException : HttpRequestException
{
    public TeslaHttpException(string? message) : base(message)
    {
    }
}