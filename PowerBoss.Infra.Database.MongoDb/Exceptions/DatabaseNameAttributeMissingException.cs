using System.Diagnostics.CodeAnalysis;
using PowerBoss.Infra.Database.MongoDb.Attributes;

namespace PowerBoss.Infra.Database.MongoDb.Exceptions;

public class DatabaseNameAttributeMissingException : Exception
{
    public DatabaseNameAttributeMissingException()
        : base("The type does not have any Database attribute.")
    {
    }

    public static void ThrowIfNull([NotNull] DatabaseAttribute? attribute)
    {
        if (attribute is null)
        {
            throw new DatabaseNameAttributeMissingException();
        }
    }

    [DoesNotReturn]
    private static void Throw(string? paramName) =>
        throw new DatabaseNameAttributeMissingException();
}