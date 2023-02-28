using System.Text.Json.Serialization;

namespace PowerBoss.Infra.Api.Tesla.Models;

public class SoftwareUpdate
{
    [JsonPropertyName("download_perc")]
    public float DownloadPerc { get; set; }

    [JsonPropertyName("expected_duration_sec")]
    public float ExpectedDurationSec { get; set; }

    [JsonPropertyName("install_perc")]
    public float InstallPerc { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("version")]
    public string Version { get; set; }
}