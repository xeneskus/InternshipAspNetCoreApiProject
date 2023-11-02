namespace MyProject.Data.Repositories.Abstract
{
    public interface IGenericRepository<TEntity> where TEntity :  class
    {
        Task<TEntity> GetByIdAsync(int id);   
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task AddAsync(TEntity entity);
        TEntity Update(TEntity entity);
        void Remove(TEntity entity);
       
   

    }
}
