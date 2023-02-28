using PowerBoss.Infra.Database.MongoDb.Attributes;

namespace PowerBoss.Infra.Database.MongoDb.Exceptions;

public class CollectionNameInvalidException : ArgumentException
{
    public CollectionNameInvalidException(string collectionName)
        : base($"Invalid collection name {collectionName}. Please use alpha characters only.")
    {
    }
}