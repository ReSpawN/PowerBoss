using FluentAssertions;
using PowerBoss.Infra.Database.MongoDb.Exceptions;
using PowerBoss.Infra.Database.MongoDb.Resolvers;

// ReSharper disable ClassNeverInstantiated.Global

namespace PowerBoss.Infra.Api.Tesla.Tests;

public class CollectionAttributeResolverTests
{
    [Fact]
    public void GetCollectionNameFromClassTest()
    {
        CollectionAttributeResolver.Resolve<CollectionNameFromClass>()
            .Should().Be(nameof(CollectionNameFromClass));
    }

    [Fact]
    public void GetCollectionNameFromAttributeTest()
    {
        CollectionAttributeResolver.Resolve<CollectionNameFromAttribute>()
            .Should().Be("FromAttribute");
    }

    [Fact]
    public void CollectionNameAttributeMissingTest()
    {
        Action comparison = () => { CollectionAttributeResolver.Resolve<CollectionNameAttributeMissing>(); };

        comparison.Should().Throw<CollectionNameAttributeMissingException>();
    }

    [Fact]
    public void CollectionNameInvalidRegexTest()
    {
        Action comparison = () => { CollectionAttributeResolver.Resolve<CollectionNameInvalidRegex>(); };

        comparison.Should().Throw<CollectionNameInvalidException>();
    }

    [Fact]
    public void GetCollectionNameFromAttributeRemovingDocumentSuffixTest()
    {
        CollectionAttributeResolver.Resolve<CollectionNameFromAttributeDocument>()
            .Should().Be("CollectionNameFromAttribute");
    }
}

internal class CollectionNameAttributeMissing
{
}

[Database.MongoDb.Attributes.Collection]
internal class CollectionNameFromClass
{
}

[Database.MongoDb.Attributes.Collection("FromAttribute")]
internal class CollectionNameFromAttribute
{
}

[Database.MongoDb.Attributes.Collection]
internal class CollectionNameFromAttributeDocument
{
}

[Database.MongoDb.Attributes.Collection("From Attribute")]
internal class CollectionNameInvalidRegex
{
}