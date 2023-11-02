using MyProject.Core.DTOs;
using MyProject.Core.DTOs.CityDtos;
using MyProject.Core.Entities;
using MyProject.Data.Repositories.Abstract;
using MyProject.Data.UnitOfWorks;
using MyProject.Service.Mapping;
using MyProject.Service.Services.Abstract;

namespace MyProject.Service.Services.Concrete
{
    public class CityService : ICityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICityRepository _cityRepository;

        public CityService(IUnitOfWork unitOfWork, ICityRepository cityRepository)
        {
            _unitOfWork = unitOfWork;
            _cityRepository = cityRepository;
        }

        public async Task<Response<CityDto>> AddAsync(CityDto entity)
        {
            var newEntity = ObjectMapper.Mapper.Map<City>(entity); 
            await _cityRepository.AddAsync(newEntity);
            await _unitOfWork.SaveAsync();

            var newDto = ObjectMapper.Mapper.Map<CityDto>(newEntity);
            return Response<CityDto>.Success(newDto, 200);
        }

        public async Task<Response<IEnumerable<CityDto>>> GetAllAsync()
        {
            var products = ObjectMapper.Mapper.Map<List<CityDto>>(await _cityRepository.GetAllAsync()); 
            return Response<IEnumerable<CityDto>>.Success(products, 200);
        }

        public async Task<Response<CityDto>> GetByIdAsync(int id)
        {
            var product = await _cityRepository.GetByIdAsync(id);

            if (product == null)
            {
                return Response<CityDto>.Fail("Id not found", 404, true); 
            }
            return Response<CityDto>.Success(ObjectMapper.Mapper.Map<CityDto>(product), 200); 

        }

        public async Task<Response<NoDataDto>> Remove(int id)
        {
            var isExistEntity = await _cityRepository.GetByIdAsync(id);
            if (isExistEntity == null)
            {
                return Response<NoDataDto>.Fail("Id not found", 404, true);
            }
            _cityRepository.Remove(isExistEntity); 
            await _unitOfWork.SaveAsync();
            return Response<NoDataDto>.Success(204);

        }

        public async Task<Response<NoDataDto>> Update(CityUpdateDto entity, int id)
        {
            var isExistEntity = await _cityRepository.GetByIdAsync(id);
            if (isExistEntity == null)
            {
                return Response<NoDataDto>.Fail("Id not fount", 404, true);
            }
            var updateEntity = ObjectMapper.Mapper.Map<City>(entity);
            _cityRepository.Update(updateEntity);
            await _unitOfWork.SaveAsync();
            return Response<NoDataDto>.Success(204); //client zaten görüyor update edilmiş datayı tekrar dönmeye gerek yok

        }

    }
}
