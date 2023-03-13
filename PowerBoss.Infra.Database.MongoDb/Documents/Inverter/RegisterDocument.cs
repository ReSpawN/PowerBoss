using System.Text.Json.Serialization;
using PowerBoss.Domain.Solar.Enums;
using PowerBoss.Domain.Solar.Models;
using PowerBoss.Infra.Database.MongoDb.Attributes;

namespace PowerBoss.Infra.Database.MongoDb.Documents.Inverter;

[Collection("registers")]
public sealed record RegisterDocument : DocumentBase
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Phase Phase { get; init; }
    public required RegisterAcCurrent AcCurrent { get; init; }
    public required RegisterAcVoltages AcVoltage { get; init; }
    public required RegisterGeneration Generation { get; init; }
    public required float ApparentPowerInVa { get; init; }
    public required float ReactivePowerInVar { get; init; }
    public required float HeatSinkTemperatureInCelsius { get; init; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required OperatingState OperatingState { get; init; }
}