﻿using PowerBoss.Infra.Serial.Solar.Enums;

namespace PowerBoss.Infra.Serial.Solar.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, Inherited = false)]
public class RegisterMappingAttribute : Attribute
{
    public uint Address { get; }
    public RegisterType Type { get; }
    public RegisterUnit Unit { get; }
    public RegisterAddressing Addressing { get; set; } = RegisterAddressing.Base1;
    public uint ScalingFactorAddress { get; set; }
    public uint Size { get; set; }

    public RegisterMappingAttribute(uint address, RegisterType type = RegisterType.Object, RegisterUnit unit = RegisterUnit.None)
    {
        Address = address;
        Type = type;
        Unit = unit;
    }

    public RegisterMappingAttribute(Type declaringType)
    {
        if (Address != 0 && Size != 0)
        {
            return;
        }

        Type = RegisterType.Object;
        Unit = RegisterUnit.None;

        List<uint> addresses = declaringType.GetProperties()
            .SelectMany(property => property
                .GetCustomAttributes(GetType(), false)
                .OfType<RegisterMappingAttribute>()
                .SelectMany(propertyRegisterMapping => new[]
                {
                    propertyRegisterMapping.Address,
                    propertyRegisterMapping.ScalingFactorAddress
                })
            )
            .Where(x => x != 0)
            .ToList();

        Address = addresses.Min();
        Size = addresses.Max() - Address + 1;
    }
}