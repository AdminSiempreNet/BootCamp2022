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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet("{productId}")]
        public async Task<IActionResult> OneAsync([FromRoute] int productId)
        {
            var result = await _productService.OneAsync(productId);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> ListAsync(int? categoryId = null, 
            string findExpression = null)
        {
            var result = await _productService.ListAsync(categoryId, findExpression);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> InsertAsync(ProductModel model)
        {
            var result = await _productService.InsertAsync(model);
            return Ok(result);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateAsync(ProductModel model)
        {
            var result = await _productService.UpdateAsync(model);
            return Ok(result);
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteAsync(int productId)
        {
            var result = await _productService.DeleteAsync(productId);
            return Ok(result);
        }
    }
}
