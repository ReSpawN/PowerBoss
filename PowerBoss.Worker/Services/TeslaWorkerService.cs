using PowerBoss.Domain.Tesla.Interfaces;
using PowerBoss.Domain.Tesla.Models;
using PowerBoss.Infra.Api.Tesla;
using PowerBoss.Infra.Api.Tesla.Models.Responses;

namespace PowerBoss.Worker.Services;

public class TeslaWorkerService
{
    private readonly TeslaClient _client;
    private readonly ITeslaTokenRepository _tokenRepository;
    private readonly ITeslaDriverRepository _driverRepository;
    private readonly ITeslaVehicleRepository _vehicleRepository;

    public TeslaWorkerService(
        TeslaClient client,
        ITeslaTokenRepository tokenRepository,
        ITeslaDriverRepository driverRepository,
        ITeslaVehicleRepository vehicleRepository
    )
    {
        _client = client;
        _tokenRepository = tokenRepository;
        _driverRepository = driverRepository;
        _vehicleRepository = vehicleRepository;
    }

    // @todo this should be moved to the domain layer, not the background service
    public async Task NoNameYet(CancellationToken ct = default)
    {
        Driver driver = await _driverRepository.FindByUlid(Ulid.Parse("01GTY24DA2SDJRP21DRTBZMCE4"));
        Token token = await _tokenRepository.FindByDriverUlid(driver.Ulid);

        // @todo should eventually be moved to Polly
        if (token.ExpiresAt is null || token.ExpiresAt <= DateTimeOffset.UtcNow)
        {
            RefreshTokenResponse refreshTokenResponse = await _client.RefreshToken(token, ct);

            await _tokenRepository.UpdateByUlid(
                token.Ulid,
                token
                    .SetAccessToken(refreshTokenResponse.AccessToken)
                    .SetRefreshToken(refreshTokenResponse.RefreshToken)
                    .SetExpireAfter(refreshTokenResponse.ExpiresAfter)
            );
        }

        IEnumerable<Vehicle> vehicles = await _vehicleRepository.FindByDriverUlid(driver.Ulid);
    }
}