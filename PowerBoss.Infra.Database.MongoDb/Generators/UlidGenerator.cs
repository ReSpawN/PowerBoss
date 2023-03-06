using MongoDB.Bson.Serialization;

namespace PowerBoss.Infra.Database.MongoDb.Generators;

public class UlidGenerator : IIdGenerator
{
    public static UlidGenerator Instance { get; } = new();

    public object GenerateId(object container, object document)
    {
        return Ulid.NewUlid();
    }

    public bool IsEmpty(object id) 
        => (Ulid)id == Ulid.Empty;
}