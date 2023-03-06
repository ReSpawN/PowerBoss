using MongoDB.Bson.Serialization;

namespace PowerBoss.Infra.Database.MongoDb.Serializers;

public class UlidSerializer : IBsonSerializer<Ulid>
{
    private static UlidSerializer? _instance;
    public static IBsonSerializer Instance => _instance ??= new UlidSerializer();

    object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args) 
        => Deserialize(context, args);

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
    {
        if (value.GetType() != ValueType)
        {
            throw new NotSupportedException($"This serializer does not accept anything else but {ValueType.FullName}");
        }

        Ulid ulid = (Ulid) value;

        context.Writer.WriteString(ulid.ToString());
    }

    public Type ValueType => typeof(Ulid);

    public Ulid Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args) 
        => Ulid.Parse(context.Reader.ReadString());

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Ulid value) 
        => context.Writer.WriteString(value.ToString());
}