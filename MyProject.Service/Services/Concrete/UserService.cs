using MyProject.Core.DTOs;
using MyProject.Core.DTOs.UserDtos;
using MyProject.Core.Entities;
using MyProject.Data.Repositories.Abstract;
using MyProject.Data.UnitOfWorks;
using MyProject.Service.Mapping;
using MyProject.Service.Services.Abstract;

namespace MyProject.Service.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public UserService(IUnitOfWork unitOfWork, IUserRepository genericRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = genericRepository;
        }

        public async Task<Response<IEnumerable<UserDto>>> GetAllAsync()
        {
            var products = ObjectMapper.Mapper.Map<List<UserDto>>(await _userRepository.GetAllAsync()); 
            return Response<IEnumerable<UserDto>>.Success(products, 200);
        }

        public async Task<Response<UserDto>> GetByIdAsync(int id)
        {
            var product = await _userRepository.GetByIdAsync(id);

            if (product == null)
            {
                return Response<UserDto>.Fail("Id not found", 404, true); //  en son trueda cliente gösterilsin mi demek evet dedik 
            }
            return Response<UserDto>.Success(ObjectMapper.Mapper.Map<UserDto>(product), 200); 

        }
  
        public async Task<Response<NoDataDto>> Remove(int id)
        {
            var isExistEntity = await _userRepository.GetByIdAsync(id);
            if (isExistEntity == null)
            {
                return Response<NoDataDto>.Fail("Id not found", 404, true);
            }
            _userRepository.Remove(isExistEntity); 
            await _unitOfWork.SaveAsync();


            return Response<NoDataDto>.Success(204);
        }

        public async Task<Response<NoDataDto>> Update(UserUpdateDto entity, int id)
        {
            var isExistEntity = await _userRepository.GetByIdAsync(id);
            if (isExistEntity == null)
            {
                return Response<NoDataDto>.Fail("Id not fount", 404, true);
            }
            var updateEntity = ObjectMapper.Mapper.Map<User>(entity);
            _userRepository.Update(updateEntity);
            await _unitOfWork.SaveAsync();

            return Response<NoDataDto>.Success(204); //client zaten görüyor update edilmiş datayı tekrar dönmeye gerek yok

        }
        public async Task<Response<string>> Register(UserRegisterDto userRegisterDto)
        {
            if (await _userRepository.UserExist(userRegisterDto.UserName)/* || await _userRepository.EmailExist(userRegisterDto.Email)*/)
            {
                return Response<string>.Fail("Kullanıcı adı zaten mevcut ", 400, true);
                // return Response<string>.Fail("Kullanıcı adı veya Email zaten mevcut", 400, true);
            }
            if (await _userRepository.EmailExist(userRegisterDto.Email))
            {
                return Response<string>.Fail("Email zaten mevcut", 400, true);
            }

            if (await _userRepository.PhoneExist(userRegisterDto.Email))
            {
                return Response<string>.Fail("Telefon numarası zaten mevcut", 400, true);
            }
            var userToCreate = new User
            {
                UserName = userRegisterDto.UserName,
                CityId = userRegisterDto.CityId,
                Email = userRegisterDto.Email,
                //Phone = userRegisterDto.Phone
            };

            var createdUser = await _userRepository.Register(userToCreate, userRegisterDto.Password);

            if (createdUser == null)
                return Response<string>.Fail("Kullanıcı kaydı sırasında bir hata oluştu", 500, true);

            return Response<string>.Success("Kayıt başarılı", 200);
        }
    }
}
