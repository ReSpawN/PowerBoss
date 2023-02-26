﻿using System.Text.Json.Serialization;
using PowerBoss.Domain.Models.Vehicle;

namespace PowerBoss.Domain.Models.Responses;

public class VehicleDriveStateResponse
{
    [JsonPropertyName("response")]
    public VehicleDriveState? State { get; set; }
}