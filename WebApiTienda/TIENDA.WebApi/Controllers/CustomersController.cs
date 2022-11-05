using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TIENDA.Data.Services;
using TIENDA.Models;

namespace TIENDA.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersService _customerService;

        public CustomersController(ICustomersService customersService)
        {
            _customerService = customersService;
        }

        [HttpGet]
        public async Task<IActionResult> ListAsync()
        {
            var result = await _customerService.ListAsync();
            return Ok(result);
        }

        [HttpGet("ByCity/{cityId}")]
        public async Task<IActionResult> ListAsync([FromRoute] int cityId)
        {
            var result = await _customerService.ListAsync(cityId);
            return Ok(result);
        }

        [HttpGet("Top/{top}")]
        public async Task<IActionResult> ListTopAsync([FromRoute] int top = 10)
        {
            var result = await _customerService.ListTopAsync(top);
            return Ok(result);
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> One([FromRoute] int customerId)
        {
            var result = await _customerService.OneAsync(customerId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] CustomerPostModel model)
        {
            var result = await _customerService.InsertAsync(model);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CustomerPostModel model)
        {
            var result = await _customerService.UpdateAsync(model);
            return Ok(result);
        }

        [HttpDelete("{customerId}")]
        public async Task<IActionResult> Delete([FromRoute] int customerId)
        {
            var result = await _customerService.DeleteAsync(customerId);
            return Ok(result);
        }
    }
}
