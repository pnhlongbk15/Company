namespace Data.Services.Interfaces
{
    public interface IEntityService<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetOneById(string id);
        Task AddOne(TEntity entity);
        Task UpdateOne(TEntity entity);
        Task DeleteOne(TEntity entity);
    }
}
