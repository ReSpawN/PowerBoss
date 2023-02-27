using System.Text.Json;
using FluentAssertions;
using PowerBoss.Domain.Models;

namespace PowerBoss.Tesla.Tests;

/// <summary>
/// Tests whether various JSON payloads can be correctly mapped to their respective objects.
/// </summary>
public class DeserializationTests
{
    [Theory]
    [InlineData("charging")]
    [InlineData("driving")]
    public void DeserializeChargeStateTest(string condition)
    {
        string json = File.ReadAllText($"Data/charge_state_{condition}.json");

        VehicleChargeState? chargingState = JsonSerializer.Deserialize<VehicleChargeState>(json);

        chargingState.Should().NotBeNull();
        chargingState?.Timestamp.Should().BeBefore(DateTimeOffset.UtcNow);
    }

    [Theory]
    [InlineData("charging")]
    [InlineData("driving")]
    public void DeserializeDriveStateTest(string condition)
    {
        string json = File.ReadAllText($"Data/drive_state_{condition}.json");

        VehicleDriveState? driveState = JsonSerializer.Deserialize<VehicleDriveState>(json);

        driveState.Should().NotBeNull();
        driveState?.Timestamp.Should().BeBefore(DateTimeOffset.UtcNow);
    }

    [Theory]
    [InlineData("charging", "mark")]
    [InlineData("driving", "natascha")]
    public void DeserializeGuiSettingsTest(string condition, string driver)
    {
        string json = File.ReadAllText($"Data/gui_settings_{condition}_by_{driver}.json");

        VehicleGuiSettings? guiSettings = JsonSerializer.Deserialize<VehicleGuiSettings>(json);

        guiSettings.Should().NotBeNull();
        guiSettings?.Timestamp.Should().BeBefore(DateTimeOffset.UtcNow);
    }

    [Theory]
    [InlineData("charging")]
    [InlineData("driving")]
    [InlineData("idle")]
    public void DeserializeVehicleStateTest(string condition)
    {
        string json = File.ReadAllText($"Data/vehicle_state_{condition}.json");

        VehicleState? state = JsonSerializer.Deserialize<VehicleState>(json);

        state.Should().NotBeNull();
        state?.Timestamp.Should().BeBefore(DateTimeOffset.UtcNow);
    }
}