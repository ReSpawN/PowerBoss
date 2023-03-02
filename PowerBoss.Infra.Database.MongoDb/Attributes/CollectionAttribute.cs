namespace PowerBoss.Infra.Database.MongoDb.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class CollectionAttribute : Attribute
{
    public string? Name { get; }

    public CollectionAttribute(string? name = null)
    {
        Name = name;
    }
}