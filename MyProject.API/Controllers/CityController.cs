using Microsoft.AspNetCore.Mvc;
using MyProject.Core.DTOs.CityDtos;
using MyProject.Service.Services.Abstract;

namespace MyProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : CustomBaseController
    {
 
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
      
        }

        [HttpGet]
        public async Task<IActionResult> GetCity()
        {

            return ActionResultInstance(await _cityService.GetAllAsync());



        }
        [HttpPost]
        public async Task<IActionResult> SaveCity(CityDto cityDto)
        {
           return ActionResultInstance(await _cityService.AddAsync(cityDto));


        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCity(CityUpdateDto cityUpdateDto, int id) 
        {
            return ActionResultInstance(await _cityService.Update(cityUpdateDto, id));


        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
           return ActionResultInstance(await _cityService.Remove(id));
           

        }
    }
}
