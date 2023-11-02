using MyProject.Core.Entities;
using MyProject.Data.Repositories.Abstract;

namespace MyProject.Data.Repositories.Concrete
{
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        


        public CityRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

    }
}
