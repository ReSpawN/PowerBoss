using AutoMapper;
using FluentAssertions;
using MongoDB.Bson;
using PowerBoss.Domain.Models;
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
        VehicleModel model = VehicleModel.CreateNew("Tessy");
        
        VehicleDocument? document = _mapper.Map<VehicleDocument>(model);

        document.Should().NotBeNull();
        document.Name.Should().Be(document.Name);
        document.Id.Should().NotBeNull().And.NotBe(ObjectId.Empty);
        document.Uuid.Should().Be(model.Uuid);
    }

    [Fact]
    public void DocumentToModelTest()
    {
        VehicleDocument document = new()
        {
            Id = ObjectId.GenerateNewId(),
            Name = "Tessy",
            Uuid = Ulid.NewUlid(),
            CreatedOn = DateTimeOffset.UtcNow
        };

        VehicleModel? model = _mapper.Map<VehicleModel>(document);

        model.Should().NotBeNull();
        model.Name.Should().Be(document.Name);
        model.Uuid.Should().Be(document.Uuid);
    }
}