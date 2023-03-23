namespace Business.Repositories.Interfaces
{
    public interface ILogic<TModel>
    {
        Task<IEnumerable<TModel>> GetAll();
        Task<TModel> GetOneById(string id);
        void AddOne(TModel model);
        void UpdateOne(string id, TModel model);
        void DeleteOne(string id);
    }
}
