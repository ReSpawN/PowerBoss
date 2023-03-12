using EasyModbus;
using PowerBoss.Infra.Serial.Solar.Attributes;
using PowerBoss.Infra.Serial.Solar.Enums;
using PowerBoss.Infra.Serial.Solar.Helpers;

namespace PowerBoss.Infra.Serial.Solar.Extensions;

public static class ModbusClientExtensions
{
    public static T Read<T>(this ModbusClient client)
    {
        RegisterMappingAttribute typeRegisterMapping = typeof(T)
            .GetCustomAttributes(typeof(RegisterMappingAttribute), false)
            .OfType<RegisterMappingAttribute>()
            .Single(registerMapping => registerMapping.Type == RegisterType.Object);

        int address = typeRegisterMapping.Addressing == RegisterAddressing.Base1
            ? (int) typeRegisterMapping.Address - 1
            : (int) typeRegisterMapping.Address;

        int[]? x = client.ReadHoldingRegisters(address, (int) typeRegisterMapping.Size);

        return Converter.Instance.Read<T>(x.AsMemory());
    }
}