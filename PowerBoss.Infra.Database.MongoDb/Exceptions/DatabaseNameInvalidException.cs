using System.Diagnostics.CodeAnalysis;

namespace PowerBoss.Infra.Database.MongoDb.Exceptions;

public class DatabaseNameInvalidException : ArgumentException
{
    public DatabaseNameInvalidException(string collectionName)
        : base($"Invalid database name {collectionName}. Please use alpha characters only.")
    {
    }

    [DoesNotReturn]
    private static void Throw(string collectionName) =>
        throw new DatabaseNameInvalidException(collectionName);
}