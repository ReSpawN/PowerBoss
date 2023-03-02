using CaseExtensions;
using FluentAssertions;
using PowerBoss.Infra.Database.MongoDb.Exceptions;
using PowerBoss.Infra.Database.MongoDb.Resolvers;

// ReSharper disable ClassNeverInstantiated.Global

namespace PowerBoss.Infra.Api.Tesla.Tests;

public class DatabaseAttributeResolverTests
{
    [Fact]
    public void GetDatabaseNameFromClassTest()
    {
        DatabaseAttributeResolver.Resolve(typeof(DatabaseNameFromClass))
            .Should().Be("databaseNameFromClass");
    }

    [Fact]
    public void GetDatabaseNameFromAttributeTest()
    {
        DatabaseAttributeResolver.Resolve(typeof(DatabaseNameFromAttribute))
            .Should().Be("fromAttribute");
    }

    [Fact]
    public void DatabaseNameAttributeMissingTest()
    {
        Action comparison = () => { DatabaseAttributeResolver.Resolve(typeof(DatabaseNameAttributeMissing)); };

        comparison.Should().Throw<DatabaseNameAttributeMissingException>();
    }

    [Fact]
    public void DatabaseNameInvalidRegexTest()
    {
        Action comparison = () => { DatabaseAttributeResolver.Resolve(typeof(DatabaseNameInvalidRegex)); };

        comparison.Should().Throw<DatabaseNameInvalidException>();
    }

    [Fact]
    public void GetDatabaseNameFromAttributeRemovingRepositorySuffixTest()
    {
        DatabaseAttributeResolver.Resolve(typeof(DatabaseNameFromAttributeRepository))
            .Should().Be("databaseNameFromAttribute");
    }
}

internal class DatabaseNameAttributeMissing
{
}

[Database.MongoDb.Attributes.Database]
internal class DatabaseNameFromClass
{
}

[Database.MongoDb.Attributes.Database("from_attribute")]
internal class DatabaseNameFromAttribute
{
}

[Database.MongoDb.Attributes.Database]
internal class DatabaseNameFromAttributeRepository
{
}

[Database.MongoDb.Attributes.Database("From Attribute")]
internal class DatabaseNameInvalidRegex
{
}