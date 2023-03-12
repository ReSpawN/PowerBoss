namespace PowerBoss.Domain.Interfaces;

public interface IRepository<T>
{
    Task<T> InsertOne(T model, CancellationToken ct = default);
    Task<T> FindByUlid(Ulid ulid);
    Task<IEnumerable<T>> FindAll();
    Task<T> UpdateByUlid(Ulid tokenUlid, T model);
}