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
    /// <summary>
    /// Gestión de ciudades
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CitiesController : ControllerBase
    {
        private readonly ICitiesService _citiesService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="citiesService"></param>
        public CitiesController(ICitiesService citiesService)
        {
            _citiesService = citiesService;
        }

        /// <summary>
        /// Retorna la lsita de ciduades
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ListAsync()
        {
            var result = await _citiesService.ListAsync();
            return Ok(result);
        }

        /// <summary>
        /// Retorna los datos del ciudad especificada mediante el id de la ciudad
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        [HttpGet("{cityId}")]
        public async Task<IActionResult> OneAsync(int cityId)
        {
            var result = await _citiesService.OneAsync(cityId);
            return Ok(result);
        }

        /// <summary>
        /// Agregar una ciudad
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> InsertAsync(CityModel model)
        {
            var result = await _citiesService.InsertAsync(model);
            return Ok(result);
        }

        /// <summary>
        /// Modificar los datos de una ciudad
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(CityModel model)
        {
            var result = await _citiesService.UpdateAsync(model);
            return Ok(result);
        }

        /// <summary>
        /// Eliminar una ciudad
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        [HttpDelete("{cityId}")]
        public async Task<IActionResult> DeleteAsync(int cityId)
        {
            var result = await _citiesService.DeleteAsync(cityId);
            return Ok(result);
        }
    }
}
