using FluentAssertions;
using PowerBoss.Infra.Database.MongoDb.Utilities;

namespace PowerBoss.Infra.Api.Tesla.Tests;

public class QueryStringConverterTests
{
    [Fact]
    public void ConvertTest()
    {
        Dictionary<string, object> fromDictionary = new()
        {
            {
                "one", "one"
            },
            {
                "two", 2
            },
            {
                "three", true
            },
            {
                "four", false
            },
            {
                "five", 5.12
            }
        };
        
        Dictionary<string, object> expectedDictionary = new()
        {
            {
                "one", "one"
            },
            {
                "two", "2"
            },
            {
                "three", "true"
            },
            {
                "four", "false"
            },
            {
                "five", "5.12"
            }
        };

        IDictionary<string, string> toDictionary = QueryStringConverter.ConvertToQueryHelperDictionary(fromDictionary);
        toDictionary.Should().HaveSameCount(fromDictionary);
        toDictionary.Should().BeEquivalentTo(expectedDictionary);
    }
}