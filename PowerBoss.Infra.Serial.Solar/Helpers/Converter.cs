using System.Reflection;
using Ardalis.GuardClauses;
using EasyModbus;
using PowerBoss.Infra.Serial.Solar.Attributes;
using PowerBoss.Infra.Serial.Solar.Enums;

namespace PowerBoss.Infra.Serial.Solar.Helpers;

public sealed class Converter
{
    private static Converter? _instance;

    private static readonly MethodInfo GenericReadMethod = typeof(Converter).GetMethod(nameof(Read), BindingFlags.Instance | BindingFlags.Public)!;
    private static readonly MethodInfo GenericCastMethod = typeof(Converter).GetMethod(nameof(Cast), BindingFlags.Static | BindingFlags.NonPublic)!;
    public static Converter Instance { get; } = _instance ??= new Converter();

    private Converter()
    {
    }

    private static T Cast<T>(object o) => (T) o;

    private static int[] GetFromMemory(Memory<int> memory, uint baseAddress, uint registerAddress, int length = 1)
        => memory.Slice((int) (registerAddress - baseAddress), length).ToArray();

    private uint ReadScaledAcc32(Memory<int> memory, uint baseAddress, RegisterMappingAttribute propertyRegisterMapping)
    {
        int[] registers = GetFromMemory(memory, baseAddress, propertyRegisterMapping.Address, 2);
        uint value = (uint) ModbusClient.ConvertRegistersToInt(registers, ModbusClient.RegisterOrder.HighLow);
        short scale = ReadInt16(memory, baseAddress, propertyRegisterMapping.ScalingFactorAddress);

        return Convert.ToUInt32(value * Math.Pow(10, scale));
    }

    private short ReadInt16(Memory<int> memory, uint baseAddress, uint registerAddress)
    {
        int[] registers = GetFromMemory(memory, baseAddress, registerAddress);

        return Convert.ToInt16(registers.First());
    }

    private int ReadInt32(Memory<int> memory, uint baseAddress, uint registerAddress)
    {
        int[] registers = GetFromMemory(memory, baseAddress, registerAddress);

        return Convert.ToInt32(registers.First());
    }

    private object ReadObject(Memory<int> memory, uint baseAddress, RegisterMappingAttribute propertyRegisterMapping, PropertyInfo property)
    {
        Memory<int> span = GetFromMemory(memory, baseAddress, propertyRegisterMapping.Address);

        object? result = GenericReadMethod.MakeGenericMethod(property.PropertyType)
            .Invoke(this, new object[]
            {
                span
            });

        Guard.Against.Null(result);

        return result;
    }

    private float ReadScaledInt16(Memory<int> memory, uint baseAddress, RegisterMappingAttribute propertyRegisterMapping)
    {
        short value = ReadInt16(memory, baseAddress, propertyRegisterMapping.Address);
        short scale = ReadInt16(memory, baseAddress, propertyRegisterMapping.ScalingFactorAddress);

        return Convert.ToSingle(value * Math.Pow(10, scale));
    }

    private float ReadScaledInt32(Memory<int> memory, uint baseAddress, RegisterMappingAttribute propertyRegisterMapping)
    {
        int value = ReadInt32(memory, baseAddress, propertyRegisterMapping.Address);
        short scale = ReadInt16(memory, baseAddress, propertyRegisterMapping.ScalingFactorAddress);

        return Convert.ToSingle(value * Math.Pow(10, scale));
    }

    private float ReadScaledUInt16(Memory<int> memory, uint baseAddress, RegisterMappingAttribute propertyRegisterMapping)
    {
        ushort value = ReadUInt16(memory, baseAddress, propertyRegisterMapping.Address);
        short scale = ReadInt16(memory, baseAddress, propertyRegisterMapping.ScalingFactorAddress);

        return Convert.ToSingle(value * Math.Pow(10, scale));
    }

    private float ReadScaledUInt32(Memory<int> memory, uint baseAddress, RegisterMappingAttribute propertyRegisterMapping)
    {
        uint value = ReadUInt16(memory, baseAddress, propertyRegisterMapping.Address);
        short scale = ReadInt16(memory, baseAddress, propertyRegisterMapping.ScalingFactorAddress);

        return Convert.ToSingle(value * Math.Pow(10, scale));
    }

    // private string ReadString(Memory<int> memory, uint baseAddress, RegisterMappingAttribute propertyRegisterMapping)
    // {
    //     Span<int> span = GetMemory(memory, baseAddress, propertyRegisterMapping.Address, (int) propertyRegisterMapping.Size).Span;
    //
    //     return Encoding.UTF8.GetString(registers).TrimEnd((char) 0x00);
    // }

    private ushort ReadUInt16(Memory<int> memory, uint baseAddress, uint registerAddress)
    {
        int[] registers = GetFromMemory(memory, baseAddress, registerAddress);

        return Convert.ToUInt16(registers.First());
    }

    private uint ReadUInt32(Memory<int> memory, uint baseAddress, uint registerAddress)
    {
        int[] registers = GetFromMemory(memory, baseAddress, registerAddress);

        return Convert.ToUInt32(registers.First());
    }

    private object ReadValue(Memory<int> memory, uint baseAddress, RegisterMappingAttribute registerMapping, PropertyInfo property)
        => registerMapping.Type switch
        {
            RegisterType.ScaledAcc32 => ReadScaledAcc32(memory, baseAddress, registerMapping),
            RegisterType.Int16 => ReadInt16(memory, baseAddress, registerMapping.Address),
            RegisterType.Int32 => ReadInt32(memory, baseAddress, registerMapping.Address),
            RegisterType.Object => ReadObject(memory, baseAddress, registerMapping, property),
            RegisterType.ScaledInt16 => ReadScaledInt16(memory, baseAddress, registerMapping),
            RegisterType.ScaledInt32 => ReadScaledInt32(memory, baseAddress, registerMapping),
            RegisterType.ScaledUInt16 => ReadScaledUInt16(memory, baseAddress, registerMapping),
            RegisterType.ScaledUInt32 => ReadScaledUInt32(memory, baseAddress, registerMapping),
            RegisterType.String => throw new NotSupportedException(),
            // RegisterType.String => ReadString(memory, baseAddress, registerMapping),
            RegisterType.UInt16 => ReadUInt16(memory, baseAddress, registerMapping.Address),
            RegisterType.UInt32 => ReadUInt32(memory, baseAddress, registerMapping.Address),
            _ => throw new NotSupportedException("Unknown register type")
        };

    private T ReadRegister<T>(Memory<int> memory, uint baseAddress, T instance, PropertyInfo property, RegisterMappingAttribute registerMapping)
    {
        object value = ReadValue(memory, baseAddress, registerMapping, property);
        object? castedValue = GenericCastMethod.MakeGenericMethod(property.PropertyType)
            .Invoke(null, new[]
            {
                value
            });

        Guard.Against.Null(castedValue);

        property.SetValue(instance, castedValue);

        return instance;
    }

    public T Read<T>(Memory<int> span)
    {
        Type type = typeof(T);
        Type registerMappingAttributeType = typeof(RegisterMappingAttribute);

        RegisterMappingAttribute typeRegisterMapping = type
            .GetCustomAttributes(registerMappingAttributeType, false)
            .OfType<RegisterMappingAttribute>()
            .First();

        return type
            .GetProperties()
            .SelectMany(property => property
                .GetCustomAttributes(registerMappingAttributeType, false)
                .OfType<RegisterMappingAttribute>()
                .Select(propertyRegisterMapping => new
                {
                    Property = property,
                    RegisterMapping = propertyRegisterMapping
                })
            )
            .Aggregate(
                Activator.CreateInstance<T>(),
                func: (instance, tuple)
                    => ReadRegister(span, typeRegisterMapping.Address, instance, tuple.Property, tuple.RegisterMapping)
            );
    }
}