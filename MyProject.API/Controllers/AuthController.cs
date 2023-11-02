using Microsoft.AspNetCore.Mvc;
using MyProject.Core.DTOs.AuthDtos;
using MyProject.Service.Services.Abstract;

namespace MyProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : CustomBaseController
    {
            private readonly IAuthService _authService;

            public AuthController(IAuthService authService) 
            {
                _authService = authService;
            }
 
            [HttpPost("login")]
            public async Task<IActionResult> Login([FromBody] AuthLoginDto authLoginDto ) 
            {
              return ActionResultInstance(await _authService.Login(authLoginDto));

            }

    }
}
