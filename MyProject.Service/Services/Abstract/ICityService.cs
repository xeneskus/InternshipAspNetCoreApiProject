using MyProject.Core.DTOs;
using MyProject.Core.DTOs.CityDtos;

namespace MyProject.Service.Services.Abstract
{
    public interface ICityService
    {
        Task<Response<CityDto>> GetByIdAsync(int id); //response sharedden geldi

        Task<Response<IEnumerable<CityDto>>> GetAllAsync();

        Task<Response<CityDto>> AddAsync(CityDto entity);

        Task<Response<NoDataDto>> Remove(int id); //geriye bir şey dönmediğimiz için nodatadto diye boş class açtık

        Task<Response<NoDataDto>> Update(CityUpdateDto entity, int id);
    }
}
