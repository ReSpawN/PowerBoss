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
        chargingState?.BatteryRange.Should().BeGreaterOrEqualTo(266.26f);
        chargingState?.ChargeEnergyAdded.Should().BeGreaterOrEqualTo(55.67f);
        chargingState?.ChargingState.Should().Be("Complete");
        chargingState?.Timestamp.Should().BeBefore(DateTimeOffset.UtcNow);
        chargingState?.UsableBatteryLevel.Should().BeGreaterOrEqualTo(90);
    }

    [Fact]
    public void DeserializeDriveStateTest()
    {
        string json = File.ReadAllText("Data/drive_state.json");
        
        var driveState = JsonSerializer.Deserialize<VehicleDriveState>(json);

        driveState.Should().NotBeNull();
        driveState?.Speed.Should().BeNull();
        driveState?.Power.Should().Be(0f);
        driveState?.Timestamp.Should().BeBefore(DateTimeOffset.UtcNow);
    }

    [Fact]
    public void DeserializeGuiSettingsTest()
    {
        string json = File.ReadAllText("Data/gui_settings.json");
        
        var guiSettings = JsonSerializer.Deserialize<VehicleGuiSettings>(json);

        guiSettings.Should().NotBeNull();
        guiSettings?.Timestamp.Should().BeBefore(DateTimeOffset.UtcNow);
    }

    [Fact]
    public void DeserializeVehicleStateTest()
    {
        string json = File.ReadAllText("Data/vehicle_state.json");
        
        var state = JsonSerializer.Deserialize<VehicleState>(json);

        state.Should().NotBeNull();
        state?.Timestamp.Should().BeBefore(DateTimeOffset.UtcNow);
    }
}