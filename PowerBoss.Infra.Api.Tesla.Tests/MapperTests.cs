using AutoMapper;
using FluentAssertions;
using MongoDB.Bson;
using PowerBoss.Domain.Tesla.Models;
using PowerBoss.Infra.Database.MongoDb.Documents.Tesla;
using PowerBoss.Infra.Database.MongoDb.Mapping;

namespace PowerBoss.Infra.Api.Tesla.Tests;

public class MapperTests
{
    private readonly IMapper _mapper;

    public MapperTests()
    {
        MapperConfiguration config = new(cfg => { cfg.AddProfile<VehicleProfile>(); });

        _mapper = config.CreateMapper();

        config.AssertConfigurationIsValid();
    }

    [Fact]
    public void ModelToDocumentTest()
    {
        Vehicle model = Vehicle.CreateNew(
            name: "Tessy",
            externalId: long.MaxValue,
            identificationNumber: "019cf016-1172-4728-92ed-fe9566450c25",
            state: "asleep"
        );
        
        VehicleDocument? document = _mapper.Map<VehicleDocument>(model);

        document.Should().NotBeNull();
        document.Name.Should().Be(document.Name);
        document.Id.Should().NotBeNull().And.NotBe(ObjectId.Empty);
        document.Ulid.Should().Be(model.Ulid);
        document.ExternalId.Should().Be(model.ExternalId);
        document.IdentificationNumber.Should().Be(model.IdentificationNumber);
        document.State.Should().Be(model.State);
        document.CreatedOn.Should().Be(model.CreatedOn);
        document.UpdatedOn.Should().Be(model.UpdatedOn);
    }

    [Fact]
    public void DocumentToModelTest()
    {
        VehicleDocument document = new()
        {
            Id = ObjectId.GenerateNewId(),
            Name = "Tessy",
            Ulid = Ulid.NewUlid(),
            CreatedOn = DateTimeOffset.UtcNow,
            State = "asleep",
            ExternalId = long.MaxValue,
            IdentificationNumber = "bbaa9f94-73cd-427b-b5f3-0635eca27fcf"
        };

        Vehicle? model = _mapper.Map<Vehicle>(document);

        model.Should().NotBeNull();
        model.Name.Should().Be(document.Name);
        model.Ulid.Should().Be(document.Ulid);
        model.ExternalId.Should().Be(document.ExternalId);
        model.IdentificationNumber.Should().Be(document.IdentificationNumber);
        model.State.Should().Be(document.State);
        model.CreatedOn.Should().Be(document.CreatedOn);
        model.UpdatedOn.Should().Be(document.UpdatedOn);
    }
}