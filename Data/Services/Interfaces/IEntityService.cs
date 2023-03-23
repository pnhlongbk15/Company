namespace Data.Services.Interfaces
{
    public interface IEntityService<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetOneById(string id);
        void AddOne(TEntity entity);
        void UpdateOne(TEntity mEntity, TEntity entity);
        void DeleteOne(TEntity entity);
    }
}
