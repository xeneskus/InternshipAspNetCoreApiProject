using MyProject.Core.DTOs;
using MyProject.Core.DTOs.AuthDtos;

namespace MyProject.Service.Services.Abstract
{
    public interface IAuthService
    {
        Task<Response<string>> Login(AuthLoginDto userForLoginDto); 
    }
}
