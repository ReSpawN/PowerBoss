using System.Reflection;
using System.Text.RegularExpressions;
using Ardalis.GuardClauses;
using CaseExtensions;
using PowerBoss.Infra.Database.MongoDb.Attributes;
using PowerBoss.Infra.Database.MongoDb.Exceptions;

namespace PowerBoss.Infra.Database.MongoDb.Resolvers;

public static class DatabaseAttributeResolver
{
    public static string Resolve(Type type)
    {
        DatabaseAttribute? attribute = type
            .GetCustomAttribute(typeof(DatabaseAttribute)) as DatabaseAttribute;

        DatabaseNameAttributeMissingException.ThrowIfNull(attribute);

        string databaseName = attribute.Name ?? type.Name;

        Guard.Against.NullOrWhiteSpace(databaseName);

        Match match = Regex.Match(databaseName, "^([a-z_\\-]+?)(?:Repository|Db|RepositoryDb|Database)?$", RegexOptions.IgnoreCase);

        if (!match.Success)
        {
            throw new DatabaseNameInvalidException(databaseName);
        }

        return (match.Groups.Values.FirstOrDefault(x => x.Value != databaseName)?.Value ?? databaseName)
            .ToCamelCase();
    }
}