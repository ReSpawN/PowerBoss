using System.ComponentModel.DataAnnotations;

namespace PowerBoss.Domain.Configuration;

public class TeslaOptions
{
    public const string Section = "Tesla";

    [Required]
    public string? FunctionalEndpoint { get; set; }

    [Required]
    public string? AccessToken { get; set; }

    [Required]
    public string? RefreshToken { get; set; }
    
    [Required]
    public string? AuthenticationEndpoint { get; set; }
}