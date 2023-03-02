namespace PowerBoss.Infra.Database.MongoDb.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class DatabaseAttribute : Attribute
{
    public string? Name { get; }

    public DatabaseAttribute(string? name = null)
    {
        Name = name;
    }
}