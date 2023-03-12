using FluentAssertions;
using PowerBoss.Infra.Serial.Solar.Attributes;
using PowerBoss.Infra.Serial.Solar.Enums;
using PowerBoss.Infra.Serial.Solar.Helpers;
using PowerBoss.Infra.Serial.Solar.Models;

namespace PowerBoss.Infra.Serial.Solar.Tests;

public class RegisterMappingAttributeTests
{
    [Fact]
    public void TestRegisterMappingRange()
    {
        Type type = typeof(RegisterMappingAddressRangeTest);

        RegisterMappingAttribute registerMapping = type.GetCustomAttributes(typeof(RegisterMappingAttribute), false)
            .OfType<RegisterMappingAttribute>()
            .Single();

        registerMapping.Should().NotBeNull();
        registerMapping.Address.Should().Be(40094);
        registerMapping.Size.Should().Be(40108 - 40094);
    }
}

[RegisterMapping(typeof(GenerationRegister))]
internal class RegisterMappingAddressRangeTest
{
    [RegisterMapping(40094, RegisterType.ScaledAcc32, RegisterUnit.WattHours, ScalingFactorAddress = 40096)]
    public uint AcLifetimeProductionInWatts { get; set; }

    [RegisterMapping(40097, RegisterType.ScaledUInt16, RegisterUnit.Amps, ScalingFactorAddress = 40098)]
    public float DcCurrentInAmps { get; set; }

    [RegisterMapping(40099, RegisterType.ScaledUInt16, RegisterUnit.Volts, ScalingFactorAddress = 40100)]
    public float DcVoltageInVolts { get; set; }

    [RegisterMapping(40101, RegisterType.ScaledInt16, RegisterUnit.Watts, ScalingFactorAddress = 40102)]
    public float DcPowerInWatts { get; set; }

    [RegisterMapping(40104, RegisterType.ScaledInt16, RegisterUnit.Celsius, ScalingFactorAddress = 40107)]
    public float HeatSinkTemperatureInCelsius { get; set; }

    [RegisterMapping(40108, RegisterType.UInt16)]
    public OperatingState OperatingState { get; set; }
}