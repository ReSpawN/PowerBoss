using PowerBoss.Domain.Interfaces;
using PowerBoss.Domain.Tesla.Models;

namespace PowerBoss.Domain.Tesla.Interfaces;

public interface ITeslaTokenRepository : IRepository<Token>
{
    Task<Token> FindByDriverUlid(Ulid ulid);
}