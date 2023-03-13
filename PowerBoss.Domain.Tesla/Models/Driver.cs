using Ardalis.GuardClauses;
using PowerBoss.Domain.Models;

namespace PowerBoss.Domain.Tesla.Models;

public sealed class Driver : ModelBase
{
    public required string Name { get; init;  }
    public required string Email { get; init;  }
}