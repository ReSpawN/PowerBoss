using System.Text.Json;
using FluentAssertions;
using PowerBoss.Domain.Models.Vehicle;

namespace PowerBoss.Tesla.Tests;

public class DeserializationTests
{
    [Fact]
    public void DeserializeChargeStateTest()
    {
        string json = File.ReadAllText("Data/charge_state.json");
        
        var chargingState = JsonSerializer.Deserialize<VehicleChargeState>(json);

        chargingState.Should().NotBeNull();
        chargingState?.BatteryLevel.Should().Be(96);
    }

    [Fact]
    public void DeserializeDriveStateTest()
    {
        string json = File.ReadAllText("Data/drive_state.json");
        
        var chargingState = JsonSerializer.Deserialize<VehicleDriveState>(json);

        chargingState.Should().NotBeNull();
        chargingState?.Speed.Should().BeNull();
        chargingState?.Power.Should().BeNull();
        chargingState?.Timestamp.Should().BeBefore(DateTimeOffset.UtcNow);
    }

    [Fact]
    public void DeserializeGuiSettingsTest()
    {
        string json = File.ReadAllText("Data/gui_settings.json");
        
        var chargingState = JsonSerializer.Deserialize<VehicleGuiSettings>(json);

        chargingState.Should().NotBeNull();
        chargingState?.Timestamp.Should().BeBefore(DateTimeOffset.UtcNow);
    }
}