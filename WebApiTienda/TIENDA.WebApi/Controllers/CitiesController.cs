using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Threading.Tasks;
using TIENDA.Core;
using TIENDA.Data.Entities;
using TIENDA.Data.Services;
using TIENDA.Data.SqlServer;
using TIENDA.Models;

namespace TIENDA.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CitiesController : ControllerBase
    {
        private readonly ICitiesService _citiesService;

        public CitiesController(ICitiesService citiesService)
        {
            _citiesService = citiesService;
        }


        [HttpGet]
        public async Task<IActionResult> ListAsync()
        {
            var result = await _citiesService.ListAsync();
            return Ok(result);
        }

        [HttpGet("{cityId}")]
        public async Task<IActionResult> OneAsync(int cityId)
        {
            var result = await _citiesService.OneAsync(cityId);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> InsertAsync(CityModel model)
        {
            var result = await _citiesService.InsertAsync(model);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(CityModel model)
        {
            var result = await _citiesService.UpdateAsync(model);
            return Ok(result);
        }

        [HttpDelete("{cityId}")]
        public async Task<IActionResult> DeleteAsync(int cityId)
        {
            var result = await _citiesService.DeleteAsync(cityId);
            return Ok(result);
        }
    }
}
