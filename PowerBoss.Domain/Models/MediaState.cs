﻿using System.Text.Json.Serialization;

namespace PowerBoss.Domain.Models;

public class MediaState
{
    [JsonPropertyName("remote_control_enabled")]
    public bool RemoteControlEnabled { get; set; }
}