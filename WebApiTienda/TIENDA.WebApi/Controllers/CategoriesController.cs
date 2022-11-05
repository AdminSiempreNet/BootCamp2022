using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TIENDA.Data.Services;
using TIENDA.Models;

namespace TIENDA.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> ListAsync()
        {
            var result = await _categoryService.ListAsync();
            return Ok(result);
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> One([FromRoute] int categoryId)
        {
            var result = await _categoryService.OneAsync(categoryId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] CategoryModel model)
        {
            var result = await _categoryService.InsertAsync(model);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CategoryModel model)
        {
            var result = await _categoryService.UpdateAsync(model);
            return Ok(result);
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> Delete([FromRoute] int categoryId)
        {
            var result = await _categoryService.DeleteAsync(categoryId);
            return Ok(result);
        }
    }
}
