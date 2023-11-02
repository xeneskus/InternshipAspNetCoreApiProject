using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyProject.Core.DTOs;
using MyProject.Core.DTOs.AuthDtos;
using MyProject.Core.Entities;
using MyProject.Data.Repositories.Abstract;
using MyProject.Service.Services.Abstract;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyProject.Service.Services.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly TokenOptions _tokenOptions;



        public AuthService(IAuthRepository authRepository, IOptions<TokenOptions> tokenOptions)
        {
            _authRepository = authRepository;
       
            _tokenOptions = tokenOptions.Value;
        }


        public async Task<Response<string>> Login(AuthLoginDto userForLoginDto)
        {
            var user = await _authRepository.Login(userForLoginDto.UserName, userForLoginDto.Password);

            if (user == null)
                return Response<string>.Fail("Geçersiz kullanıcı adı veya şifre", 401, true);


            var token = GenerateToken(user);

            return Response<string>.Success(token, 200);
        }

        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenOptions.SecurityKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
