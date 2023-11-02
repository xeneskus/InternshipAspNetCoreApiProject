using MyProject.Data.Repositories.Abstract;

namespace MyProject.Data.UnitOfWorks
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        ICityRepository Cities { get; }//_unitOfWork.Cities.AddAsync(city)
        IUserRepository Users { get; }
        IAuthRepository Auths { get; }
        Task<int> SaveAsync();//_unitOfWork.SaveAsync();

    }
}
