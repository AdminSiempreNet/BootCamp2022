using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TIENDA.Data.Services;
using TIENDA.Models;

namespace TIENDA.WebApi.Controllers
{

    /// <summary>
    /// Prueba
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="categoryService"></param>
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Retorna la lista de categorías
        /// </summary>
        /// <returns></returns>      
        [HttpGet]
        public async Task<IActionResult> ListAsync()
        {
            var result = await _categoryService.ListAsync();
            return Ok(result);
        }

        /// <summary>
        /// Retorna los datos de una categoría dado el id de la categoría
        /// </summary>
        /// <param name="categoryId"></param>
        [HttpGet("{categoryId}")]
        public async Task<IActionResult> One([FromRoute] int categoryId)
        {
            var result = await _categoryService.OneAsync(categoryId);
            return Ok(result);
        }

        /// <summary>
        /// Agregar una categoría
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] CategoryModel model)
        {
            var result = await _categoryService.InsertAsync(model);
            return Ok(result);
        }

        /// <summary>
        /// Modificar una categoría
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CategoryModel model)
        {
            var result = await _categoryService.UpdateAsync(model);
            return Ok(result);
        }

        /// <summary>
        /// Eliminar una categoría
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> Delete([FromRoute] int categoryId)
        {
            var result = await _categoryService.DeleteAsync(categoryId);
            return Ok(result);
        }
    }
}
