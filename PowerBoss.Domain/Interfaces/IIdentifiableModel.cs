namespace PowerBoss.Domain.Interfaces;

public interface IIdentifiableModel
{
    public Ulid Ulid { get; init; }
    public DateTimeOffset? CreatedAt { get; init; }
    public DateTimeOffset? UpdatedAt { get; set; }
}