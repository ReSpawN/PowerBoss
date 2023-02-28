namespace PowerBoss.Infra.Database.MongoDb.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class Collection : Attribute
{
    public string? Name { get; }

    public Collection(string? name = null)
    {
        Name = name;
    }
}