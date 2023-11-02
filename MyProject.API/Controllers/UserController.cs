using MassTransit;
using Microsoft.AspNetCore.Mvc;
using MyProject.Consumer;
using MyProject.Core.DTOs.UserDtos;
using MyProject.Service.Services.Abstract;

namespace MyProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : CustomBaseController
    {
        private readonly IUserService _userService;
        private readonly IBusControl _busControl;
        public UserController(IUserService userService, IBusControl busControl)
        {
            _userService = userService;
            _busControl = busControl;
        }
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            // Kullanıcıları getirme işlemi öncesinde log oluştur
            var logModel = new LogModel
            {
                Namespace = "MyProject.API.Controllers.UserController",
                MethodName = "GetUser",
                Request = "GetUser"  // Get işleminin bilgilerini burada taşı
            };

            //// Logu RabbitMQ'ya gönder
            await _busControl.Publish(logModel);

            return ActionResultInstance(await _userService.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {


            return ActionResultInstance(await _userService.GetByIdAsync(id));
        }
      
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(UserUpdateDto userUpdateDto, int id)
        {

            return ActionResultInstance(await _userService.Update(userUpdateDto, id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            return ActionResultInstance(await _userService.Remove(id));
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        {

            return ActionResultInstance(await _userService.Register(userRegisterDto));
        }
   

    }

}
