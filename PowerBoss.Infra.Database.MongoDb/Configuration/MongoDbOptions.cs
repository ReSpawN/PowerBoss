using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.WebUtilities;
using MongoDB.Driver;
using PowerBoss.Infra.Database.MongoDb.Utilities;

namespace PowerBoss.Infra.Database.MongoDb.Configuration;

public class MongoDbOptions
{
    public const string Section = "MongoDb";

    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string Host { get; set; }

    [Required]
    public int Port { get; set; } = 27017;

    [Required]
    public string Scheme { get; set; } = "mongodb+srv";

    public IDictionary<string, object> Properties { get; set; }

    public string ToConnectionString()
    {
        string connectionString = $"{Scheme}://{UserName}:{Password}@{Host}";

        if (!Scheme.Contains("+srv"))
            connectionString += $":{Port}";
        
        return QueryHelpers.AddQueryString(connectionString, QueryStringConverter.ConvertToQueryHelperDictionary(Properties));
    }
}