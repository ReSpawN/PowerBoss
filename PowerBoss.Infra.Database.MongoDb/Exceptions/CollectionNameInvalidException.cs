using System.Diagnostics.CodeAnalysis;

namespace PowerBoss.Infra.Database.MongoDb.Exceptions;

public class CollectionNameInvalidException : ArgumentException
{
    public CollectionNameInvalidException(string collectionName)
        : base($"Invalid collection name {collectionName}. Please use alpha characters only.")
    {
    }

    [DoesNotReturn]
    private static void Throw(string collectionName) =>
        throw new CollectionNameInvalidException(collectionName);
}