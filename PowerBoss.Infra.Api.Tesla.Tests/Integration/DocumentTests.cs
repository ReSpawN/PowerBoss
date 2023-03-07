﻿using FluentAssertions;
using MongoDB.Driver;
using PowerBoss.Domain.Tesla.Interfaces;
using PowerBoss.Domain.Tesla.Models;
using PowerBoss.Infra.Database.MongoDb.Documents.Tesla;
using Xunit.Abstractions;

namespace PowerBoss.Infra.Api.Tesla.Tests.Integration;

public class DocumentTests
{
    private readonly ITestOutputHelper _outputHelper;
    private readonly ITeslaDriverRepository _driverRepository;
    private readonly ITeslaVehicleRepository _vehicleRepository;

    public DocumentTests(
        ITestOutputHelper outputHelper, 
        ITeslaDriverRepository driverRepository, 
        ITeslaVehicleRepository vehicleRepository 
        )
    {
        _outputHelper = outputHelper;
        _driverRepository = driverRepository;
        _vehicleRepository = vehicleRepository;
    }

    [Fact]
    public async Task MongoConventionTest()
    {
        Driver driver = Driver.CreateNew("Mark", "no-reply@domain.tld");
        Driver insertedDriverDocument = await _driverRepository.InsertOne(driver);
        Driver fetchedDriverDocument = await _driverRepository.FindByUlid(insertedDriverDocument.Ulid);
        
        insertedDriverDocument.Should().BeEquivalentTo(fetchedDriverDocument);
        
        Vehicle vehicle = Vehicle.CreateNew(
            driverUlid: insertedDriverDocument.Ulid,
            name: "TessyFromTest",
            externalId: long.MaxValue,
            identificationNumber: "019cf016-1172-4728-92ed-fe9566450c25",
            state: "asleep"
        );
        
        Vehicle insertedVehicleDocument = await _vehicleRepository.InsertOne(vehicle);
        Vehicle fetchedVehicleDocument = await _vehicleRepository.FindByUlid(vehicle.Ulid);
        
        insertedVehicleDocument.Should().BeEquivalentTo(fetchedVehicleDocument);
        insertedVehicleDocument.DriverUlid.Should().Be(driver.Ulid);
    }
}