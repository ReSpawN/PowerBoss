using FluentAssertions;
using MongoDB.Driver;
using PowerBoss.Domain.Tesla.Interfaces;
using PowerBoss.Domain.Tesla.Models;
using Xunit.Abstractions;

namespace PowerBoss.Infra.Api.Tesla.Tests.Integration;

public class DocumentTests
{
    private readonly ITestOutputHelper _outputHelper;
    private readonly ITeslaVehicleRepository _repository;

    public DocumentTests(ITestOutputHelper outputHelper, ITeslaVehicleRepository repository)
    {
        _outputHelper = outputHelper;
        _repository = repository;
    }

    [Fact]
    public async Task MongoConventionTest()
    {
        Vehicle vehicle = Vehicle.CreateNew(
            name: "TessyFromTest",
            externalId: long.MaxValue,
            identificationNumber: "019cf016-1172-4728-92ed-fe9566450c25",
            state: "asleep"
        );
        
        Vehicle insertedDocument = await _repository.InsertOne(vehicle);
        Vehicle fetchedDocument = await _repository.FindByUlid(vehicle.Ulid);
        
        insertedDocument.Should().BeEquivalentTo(fetchedDocument);
    }
}