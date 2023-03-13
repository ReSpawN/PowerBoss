using System.Text.Json;
using AutoMapper;
using FluentAssertions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using PowerBoss.Domain.Solar.Enums;
using PowerBoss.Domain.Solar.Models;
using PowerBoss.Infra.Database.MongoDb.Documents.Inverter;
using PowerBoss.Infra.Serial.Solar.Enums;
using PowerBoss.Infra.Serial.Solar.Dtos;

namespace PowerBoss.Infra.Serial.Solar.Tests;

public class MapperTests
{
    private readonly IMapper _mapper;

    public MapperTests(IMapper mapper)
    {
        _mapper = mapper;
    }

    /// <summary>
    /// Simulates a repository document fetch where the MongoId is unknown and unimportant to the Domain.
    /// </summary>
    [Fact]
    public void FromSerialToDatabase()
    {
        #region Serial MODBUS

        InverterRegisterDto? infraRegisterDto = JsonSerializer.Deserialize<InverterRegisterDto>(File.OpenRead("Data/register.json"));
        Assert.NotNull(infraRegisterDto);
        Register domainRegisterDto = _mapper.Map<InverterRegisterDto, Register>(infraRegisterDto);
        
        #endregion

        #region Repository persistence

        RegisterDocument infraPendingPersistDocument = _mapper.Map<Register, RegisterDocument>(domainRegisterDto);
        Register? domainCreatedRegisterDto = _mapper.Map<RegisterDocument, Register>(infraPendingPersistDocument);

        #endregion

        domainCreatedRegisterDto.CreatedAt.Should().NotBeNull();

        domainCreatedRegisterDto.Ulid.Should().Be(infraPendingPersistDocument.Ulid);
        infraPendingPersistDocument.Ulid.Should().Be(domainCreatedRegisterDto.Ulid);

        domainCreatedRegisterDto.UpdatedAt.Should().BeNull();
    }

    /// <summary>
    /// Simulates a repository document fetch where the MongoId is unknown and unimportant to the Domain.
    /// </summary>
    [Fact]
    public void FindToDomainUpdateToFlushTests()
    {
        #region Repository fetch

        var x = BsonSerializer.Deserialize<RegisterDocument>(File.ReadAllText("Data/document.json"));

        // Simulate a record coming from the database
        RegisterDocument? registerDocument = JsonSerializer.Deserialize<RegisterDocument>(File.OpenRead("Data/document.json"), new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
        Assert.NotNull(registerDocument);

        Register? registerModel = _mapper.Map<RegisterDocument, Register>(registerDocument);

        #endregion

        #region Domain

        // registerModel.Phase = (ushort) Phase.SplitPhase;

        #endregion

        #region Repository persistence

        RegisterDocument pendingFlushRegisterDocument = _mapper.Map<Register, RegisterDocument>(registerModel);
        pendingFlushRegisterDocument.UpdatedAt = DateTimeOffset.UtcNow;
        Register? updatedRegisterModel = _mapper.Map<RegisterDocument, Register>(pendingFlushRegisterDocument);

        #endregion

        registerDocument.CreatedAt.Should().Be(pendingFlushRegisterDocument.CreatedAt);

        registerDocument.Ulid.Should().Be(registerModel.Ulid);
        registerModel.Ulid.Should().Be(pendingFlushRegisterDocument.Ulid);

        updatedRegisterModel.UpdatedAt.Should().NotBeNull();
        updatedRegisterModel.UpdatedAt.Should().Be(pendingFlushRegisterDocument.UpdatedAt);
    }
}