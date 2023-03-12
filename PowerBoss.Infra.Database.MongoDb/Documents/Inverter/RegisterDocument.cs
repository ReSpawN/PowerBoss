using PowerBoss.Infra.Database.MongoDb.Attributes;

namespace PowerBoss.Infra.Database.MongoDb.Documents.Inverter;

[Collection("registers")]
public sealed class RegisterDocument : DocumentBase
{
    public required ushort Phase { get; init; }
}