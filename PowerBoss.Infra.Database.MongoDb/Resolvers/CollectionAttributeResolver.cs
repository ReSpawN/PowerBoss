using System.Reflection;
using System.Text.RegularExpressions;
using Ardalis.GuardClauses;
using PowerBoss.Infra.Database.MongoDb.Attributes;
using PowerBoss.Infra.Database.MongoDb.Exceptions;

namespace PowerBoss.Infra.Database.MongoDb.Resolvers;

public static class CollectionAttributeResolver
{
    public static string Resolve<T>()
    {
        Type type = typeof(T);
        Collection? attribute = type
            .GetCustomAttribute(typeof(Collection)) as Collection;

        CollectionNameAttributeMissingException.ThrowIfNull(attribute);

        string collectionName = attribute.Name ?? type.Name;

        Guard.Against.NullOrWhiteSpace(collectionName);

        if (!Regex.Match(collectionName, "^[a-z]+$", RegexOptions.IgnoreCase).Success)
        {
            throw new CollectionNameInvalidException(collectionName);
        }

        return collectionName;
    }
}