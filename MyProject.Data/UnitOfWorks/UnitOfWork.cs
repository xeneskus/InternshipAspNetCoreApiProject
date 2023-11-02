using MyProject.Data.Repositories.Abstract;
using MyProject.Data.Repositories.Concrete;

namespace MyProject.Data.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private UserRepository _userRepository;
        private AuthRepository _authRepository;
        private CityRepository _cityRepository;


        public UnitOfWork(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public ICityRepository Cities => _cityRepository ?? new CityRepository(_context);

        public IUserRepository Users => _userRepository ?? new UserRepository(_context);

        public IAuthRepository Auths => _authRepository ?? new AuthRepository(_context);//kullanıcı bizden isterse eğer varsa dönüyoruz yoksa yeni oluşturup onu dönüyoruz

        public async ValueTask DisposeAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
