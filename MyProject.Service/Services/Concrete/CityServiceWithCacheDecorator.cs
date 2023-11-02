using MyProject.Cache;
using MyProject.Core.Entities;
using MyProject.Data.Repositories.Abstract;
using StackExchange.Redis;
using System.Linq.Expressions;
using System.Text.Json;

namespace MyProject.Data.Repositories.Concrete
{//hash olarak tuttuk veri tipi çünkü key üzerinden dataları çok hızlı alırız Id=> value id üzerinden bir arama yapınca çok hızlı alabiliriz diğer türlü ilk önce memorye alıp sonra memory üzerinden datayı arıyoruz hashte direkt id üzerinden alıyoruz
    public class CityServiceWithCacheDecorator : ICityRepository
    {
        private const string cityKey = "cityCaches";
        private readonly ICityRepository _cityRepository;
        private readonly RedisService _redisService;
        private readonly IDatabase _cacheRepository;
        public CityServiceWithCacheDecorator(ICityRepository cityRepository, RedisService redisService)
        {
            _cityRepository = cityRepository;
            _redisService = redisService;
            _cacheRepository = _redisService.GetDb(1);
        }

        public Task AddAsync(City entity)
        {
            //if (await _cacheRepository.KeyExistsAsync(cityKey))
            //{
            //    //await _cacheRepository.HashSetAsync(cityKey, entity.Id, JsonSerializer.Serialize(newCity));

            //}
            //return newCity;


            //await _cityRepository.AddAsync(entity);
            //await _cacheRepository.HashSetAsync(cityKey, entity.Id, JsonSerializer.Serialize(entity));
            throw new NotImplementedException();

        }

        public async Task<IEnumerable<City>> GetAllAsync()
        {
            //burdan cevabı alınca returnle
            if (await _cacheRepository.KeyExistsAsync(cityKey)) 
            {

                var city = await _cacheRepository.HashGetAllAsync(cityKey);
                return city.Select(x => JsonSerializer.Deserialize<City>(x.Value)).ToList();
              
                //redisten çek dön //önce db yüklensin sonra bana cachle gelsin
            }
         

            var cities = await LoadToCacheFromDbAsync();
            return cities.ToList(); 
            //dbden al

        }

        public async Task<City> GetByIdAsync(int id)
        {
            if (_cacheRepository.KeyExists(cityKey))
            {
                var city = await _cacheRepository.HashGetAsync(cityKey,id);         
                return JsonSerializer.Deserialize<City>(city);
            
            }
            var cities = await LoadToCacheFromDbAsync();
            return cities.FirstOrDefault(x => x.Id == id);
        }

        public Task<bool> IsExistAsync(Expression<Func<City, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public void Remove(City entity)
        {
            _cityRepository.Remove(entity);
            _cacheRepository.HashDelete(cityKey, entity.Id);
    
        }

        public City Update(City entity)
        {
            //var updateCity = _cityRepository.Update(entity);
            //_cacheRepository.HashSetAsync(cityKey, entity.Id, JsonSerializer.Serialize(updateCity));
            //return updateCity;
            throw new NotImplementedException();
        }
        private async Task<IEnumerable<City>> LoadToCacheFromDbAsync() 
        {
            var cities = await _cityRepository.GetAllAsync();

            foreach (var city in cities)
            {
             
                await _cacheRepository.HashSetAsync(cityKey, city.Id, JsonSerializer.Serialize(city));
                await _cacheRepository.KeyExpireAsync(cityKey, TimeSpan.FromMinutes(1));
            
            }

            return cities;
        }
    }
}
