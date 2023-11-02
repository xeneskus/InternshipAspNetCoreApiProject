using MyProject.Core.Entities;

namespace MyProject.Data.Repositories.Abstract
{
    public interface IUserRepository : IGenericRepository<User>
    {

     
        Task<User> Register(User user, string password);
        Task<bool> UserExist(string userName);

        Task<bool> EmailExist(string email);

        Task<bool> PhoneExist(string password);
    }
}
