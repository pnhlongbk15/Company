namespace Data.Services.Interfaces
{
    public interface IEntityService<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetOneById(string id);
        void AddOne(TEntity entity);
        void UpdateOne(TEntity mEntity, TEntity entity);
        void DeleteOne(TEntity entity);
    }
}
