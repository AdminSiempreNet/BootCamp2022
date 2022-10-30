using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using TIENDA.Data.Entities;
using TIENDA.Data.SqlServer;
using TIENDA.Models;

namespace TIENDA.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly DBConnection _context;

        public CitiesController(DBConnection context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] CityModel model)
        {
            City city = new City
            {
                Name = model.Name
            };

            _context.Cities.Add(city);

            var result = await _context.SaveAsync();

            if (result.IsSuccess)
            {
                result.Message = "Ciudad creada correctamente";
            }
            else
            {
                result.Message = "Error al registrar la ciudad";
            }

            return Ok(result);
        }
    }
}
