using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TIENDA.Core;
using TIENDA.Data.Entities;
using TIENDA.Data.Services;
using TIENDA.Models;

namespace TIENDA.WebApi.Controllers
{
    /// <summary>
    /// Gestión de productos
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="productService"></param>
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Datos del producto especificado
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet("{productId}")]
        public async Task<IActionResult> OneAsync([FromRoute] int productId)
        {
            var result = await _productService.OneAsync(productId);
            return Ok(result);
        }

        /// <summary>
        /// Lista de prodcutos, filtrados por categoría o mediante una expresión de búsqueda
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="findExpression"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ListAsync(int? categoryId = null, 
            string findExpression = null)
        {
            var result = await _productService.ListAsync(categoryId, findExpression);
            return Ok(result);
        }   

        /// <summary>
        /// Agregar un nuevo producto
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> InsertAsync(ProductModel model)
        {
            var result = await _productService.InsertAsync(model);
            return Ok(result);
        }

        /// <summary>
        /// Modificar los datos de un producto
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(ProductModel model)
        {
            var result = await _productService.UpdateAsync(model);
            return Ok(result);
        }

        /// <summary>
        /// Eliminar un prodcuto
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteAsync(int productId)
        {
            var result = await _productService.DeleteAsync(productId);
            return Ok(result);
        }
    }
}
