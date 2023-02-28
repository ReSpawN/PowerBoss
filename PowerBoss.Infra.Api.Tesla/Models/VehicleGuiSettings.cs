using System.Text.Json.Serialization;
using PowerBoss.Infra.Api.Tesla.Converters;

namespace PowerBoss.Infra.Api.Tesla.Models;

public class VehicleGuiSettings
{
    [JsonPropertyName("gui_24_hour_time")]
    public bool Gui24HourTime { get; set; }

    [JsonPropertyName("gui_charge_rate_units")]
    public string? GuiChargeRateUnits { get; set; }

    [JsonPropertyName("gui_distance_units")]
    public string? GuiDistanceUnits { get; set; }

    [JsonPropertyName("gui_range_display")]
    public string? GuiRangeDisplay { get; set; }

    [JsonPropertyName("gui_temperature_units")]
    public string? GuiTemperatureUnits { get; set; }

    [JsonPropertyName("show_range_units")]
    public bool ShowRangeUnits { get; set; }

    [JsonPropertyName("timestamp")]
    [JsonConverter(typeof(TimestampToDateTimeOffsetJsonConverter))]
    public DateTimeOffset? Timestamp { get; set; }
}