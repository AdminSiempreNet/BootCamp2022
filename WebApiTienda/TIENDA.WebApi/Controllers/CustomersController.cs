using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TIENDA.Data.Services;
using TIENDA.Models;

namespace TIENDA.WebApi.Controllers
{
    /// <summary>
    /// Gestión de clientes
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersService _customerService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="customersService"></param>
        public CustomersController(ICustomersService customersService)
        {
            _customerService = customersService;
        }

        /// <summary>
        /// Lista de todos los clientes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ListAsync()
        {
            var result = await _customerService.ListAsync();
            return Ok(result);
        }

        /// <summary>
        /// Lista de clientes filtrado por el id de la ciudad
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        [HttpGet("ByCity/{cityId}")]
        public async Task<IActionResult> ListAsync([FromRoute] int cityId)
        {
            var result = await _customerService.ListAsync(cityId);
            return Ok(result);
        }

        /// <summary>
        /// Top (por defecto 10) de clientes con mayor número de facturas
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        [HttpGet("Top/{top}")]
        public async Task<IActionResult> ListTopAsync([FromRoute] int top = 10)
        {
            var result = await _customerService.ListTopAsync(top);
            return Ok(result);
        }

        /// <summary>
        /// Datos de un cliente
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet("{customerId}")]
        public async Task<IActionResult> One([FromRoute] int customerId)
        {
            var result = await _customerService.OneAsync(customerId);
            return Ok(result);
        }

        /// <summary>
        /// Agregar un nuevo cliente
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] CustomerPostModel model)
        {
            var result = await _customerService.InsertAsync(model);
            return Ok(result);
        }

        /// <summary>
        /// Modificar los datos de un cliente
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CustomerPostModel model)
        {
            var result = await _customerService.UpdateAsync(model);
            return Ok(result);
        }

        /// <summary>
        /// Eliminar un cliente
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpDelete("{customerId}")]
        public async Task<IActionResult> Delete([FromRoute] int customerId)
        {
            var result = await _customerService.DeleteAsync(customerId);
            return Ok(result);
        }
    }
}
