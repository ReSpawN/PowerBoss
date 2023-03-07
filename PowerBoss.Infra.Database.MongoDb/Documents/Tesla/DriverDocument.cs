﻿using PowerBoss.Infra.Database.MongoDb.Attributes;

namespace PowerBoss.Infra.Database.MongoDb.Documents.Tesla;

[Collection("driver")]
public sealed class DriverDocument : DocumentBase
{
    public required string Name { get; set; }
    public required string Email { get; set; }
}