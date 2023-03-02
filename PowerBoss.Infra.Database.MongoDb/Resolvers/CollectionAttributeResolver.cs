using System.Reflection;
using System.Text.RegularExpressions;
using Ardalis.GuardClauses;
using CaseExtensions;
using PowerBoss.Infra.Database.MongoDb.Attributes;
using PowerBoss.Infra.Database.MongoDb.Exceptions;

namespace PowerBoss.Infra.Database.MongoDb.Resolvers;

public static class CollectionAttributeResolver
{
    public static string Resolve<T>()
    {
        Type type = typeof(T);
        CollectionAttribute? attribute = type
            .GetCustomAttribute(typeof(CollectionAttribute)) as CollectionAttribute;

        CollectionNameAttributeMissingException.ThrowIfNull(attribute);

        string collectionName = attribute.Name ?? type.Name;

        Guard.Against.NullOrWhiteSpace(collectionName);

        Match match = Regex.Match(collectionName, "^([a-z_\\-]+?)(?:Document)?$", RegexOptions.IgnoreCase);

        if (!match.Success)
        {
            throw new CollectionNameInvalidException(collectionName);
        }

        return (match.Groups.Values.FirstOrDefault(x => x.Value != collectionName)?.Value ?? collectionName)
            .ToCamelCase();
    }
}