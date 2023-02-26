using System.ComponentModel.DataAnnotations;

namespace PowerBoss.Domain.Configuration;

public class TeslaOptions
{
    public const string Section = "Tesla";

    [Required]
    public string? Endpoint { get; set; }

    [Required]
    public string? AccessToken { get; set; }

    [Required]
    public string? RefreshToken { get; set; }
}