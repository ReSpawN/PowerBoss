using System.Text.Json.Serialization;
using PowerBoss.Domain.Converters;

namespace PowerBoss.Domain.Models;

public class VehicleDriveState
{
    [JsonPropertyName("active_route_latitude")]
    public double? ActiveRouteLatitude { get; set; }

    [JsonPropertyName("active_route_longitude")]
    public double? ActiveRouteLongitude { get; set; }

    [JsonPropertyName("active_route_traffic_minutes_delay")]
    public float? ActiveRouteTrafficMinutesDelay { get; set; }

    [JsonPropertyName("gps_as_of")]
    [JsonConverter(typeof(TimestampToDateTimeOffsetJsonConverter))]
    public DateTimeOffset? GpsAsOf { get; set; }

    [JsonPropertyName("heading")]
    public float? Heading { get; set; }

    [JsonPropertyName("latitude")]
    public double? Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double? Longitude { get; set; }

    [JsonPropertyName("native_latitude")]
    public double? NativeLatitude { get; set; }

    [JsonPropertyName("native_location_supported")]
    public int? NativeLocationSupported { get; set; }

    [JsonPropertyName("native_longitude")]
    public double? NativeLongitude { get; set; }

    [JsonPropertyName("native_type")]
    public string? NativeType { get; set; }

    [JsonPropertyName("power")]
    public float? Power { get; set; }

    [JsonPropertyName("shift_state")]
    public string? ShiftState { get; set; }

    [JsonPropertyName("speed")]
    public float? Speed { get; set; }

    [JsonPropertyName("timestamp")]
    [JsonConverter(typeof(TimestampToDateTimeOffsetJsonConverter))]
    public DateTimeOffset? Timestamp { get; set; }
}