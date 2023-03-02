using System.Diagnostics.CodeAnalysis;
using PowerBoss.Infra.Database.MongoDb.Attributes;

namespace PowerBoss.Infra.Database.MongoDb.Exceptions;

public class CollectionNameAttributeMissingException : Exception
{
    public CollectionNameAttributeMissingException()
        : base("The type does not have any Collection attribute.")
    {
    }

    public static void ThrowIfNull([NotNull] CollectionAttribute? attribute)
    {
        if (attribute is null)
        {
            throw new CollectionNameAttributeMissingException();
        }
    }

    [DoesNotReturn]
    private static void Throw(string? paramName) =>
        throw new CollectionNameAttributeMissingException();
}