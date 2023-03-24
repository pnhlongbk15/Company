namespace Business.Repositories.Interfaces
{
    public interface ILogic<TModel>
    {
        Task<IEnumerable<TModel>> GetAll();
        Task<TModel> GetOneById(string id);
        Task AddOne(TModel model);
        Task UpdateOne(TModel model);
        Task DeleteOne(string id);
        Task DeleteOneByProcedure(string email, string departmentName);
    }
}
