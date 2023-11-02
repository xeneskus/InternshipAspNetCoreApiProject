using Microsoft.EntityFrameworkCore;
using MyProject.Data.Repositories.Abstract;

namespace MyProject.Data.Repositories.Concrete
{
    public class GenericRepository<Tentity> : IGenericRepository<Tentity> where Tentity : class
    {
        protected readonly AppDbContext _appDbContext;
        protected readonly DbSet<Tentity> _dbSet;

        public GenericRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _dbSet = _appDbContext.Set<Tentity>();
        }

        public async Task AddAsync(Tentity entity)
        {
            await _dbSet.AddAsync(entity);
        }


        public async Task<IEnumerable<Tentity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<Tentity> GetByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity!=null)
            {
                _appDbContext.Entry(entity).State = EntityState.Detached;
            }
            return entity;
        }

        public void Remove(Tentity entity)
        {
            _dbSet.Remove(entity);
        }


        public Tentity Update(Tentity entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Modified; //aynı olsa dahi tüm alanları günceller
            return entity;
        }
   
    }
}
