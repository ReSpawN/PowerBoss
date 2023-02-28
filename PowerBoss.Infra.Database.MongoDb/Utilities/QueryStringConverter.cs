using System.Globalization;

namespace PowerBoss.Infra.Database.MongoDb.Utilities;

public static class QueryStringConverter
{
    public static IDictionary<string, string> ConvertToQueryHelperDictionary(IDictionary<string, object> properties)
        => properties
            .Select(pair =>
            {
                string stringifyValue = pair.Value switch
                {
                    string value => value,
                    bool booleanValue => Convert.ToString(booleanValue).ToLower(),
                    int intValue => Convert.ToString(intValue),
                    double doubleValue => Convert.ToString(doubleValue, CultureInfo.InvariantCulture),
                    _ => throw new ArgumentOutOfRangeException(nameof(stringifyValue), "Unsupported conversion")
                };

                return new KeyValuePair<string, string>(pair.Key, stringifyValue);
            })
            .ToDictionary(pair => pair.Key, pair => pair.Value);
}