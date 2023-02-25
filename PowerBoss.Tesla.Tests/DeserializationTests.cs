using System.Text.Json;
using FluentAssertions;
using PowerBoss.Domain.Models.Vehicle;

namespace PowerBoss.Tesla.Tests;

public class DeserializationTests
{
    [Fact]
    public void DeserializeChargeStateTest()
    {
        string json =  File.ReadAllText("Data/charge_state.json");
        var chargingState = JsonSerializer.Deserialize<VehicleChargeState>(json);

        chargingState.Should().NotBeNull();
        chargingState?.BatteryLevel.Should().Be(96);
    }
}