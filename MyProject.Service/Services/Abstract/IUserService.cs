using MyProject.Core.DTOs;
using MyProject.Core.DTOs.UserDtos;

namespace MyProject.Service.Services.Abstract
{
    public interface IUserService
    {
        Task<Response<UserDto>> GetByIdAsync(int id); //response sharedden geldi

        Task<Response<IEnumerable<UserDto>>> GetAllAsync();

        //Task<Response<UserDto>> AddAsync(UserDto entity);

        Task<Response<NoDataDto>> Remove(int id); //geriye bir şey dönmediğimiz için nodatadto diye boş class açtık

        Task<Response<NoDataDto>> Update(UserUpdateDto entity, int id);
        Task<Response<string>> Register(UserRegisterDto userRegisterDto);
    }
}
