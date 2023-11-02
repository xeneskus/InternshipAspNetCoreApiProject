using Microsoft.EntityFrameworkCore;
using MyProject.Core.Entities;
using MyProject.Data.Repositories.Abstract;

namespace MyProject.Data.Repositories.Concrete
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }


        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            await _appDbContext.Users.AddAsync(user);
            await _appDbContext.SaveChangesAsync();
            return user;
        }
        public async Task<bool> UserExist(string userName)
        {
            return await _appDbContext.Users.AnyAsync(x => x.UserName == userName);
        }

        public async Task<bool> EmailExist(string email)
        {
            return await _appDbContext.Users.AnyAsync(x => x.Email == email);
        }

        public async Task<bool> PhoneExist(string phone)
        {
            return await _appDbContext.Users.AnyAsync(x => x.Phone == phone);

        }
    }
}
