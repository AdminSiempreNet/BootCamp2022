using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using TIENDA.Core;
using TIENDA.Models;
using TIENDA.Data.Services;
using Microsoft.AspNetCore.Authorization;

namespace TIENDA.WebApi.Controllers
{
    /// <summary>
    /// Gestión de facturas
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BillingController : ControllerBase
    {
        private readonly IBillingService _billingService;

        public BillingController(IBillingService billingService)
        {
            _billingService = billingService;
        }

        /// <summary>
        /// Lista de facturas por cliente
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet("ByCustomer/{customerId}")]
        public async Task<IActionResult> ListByCustomerAsync(int customerId)
        {
            var result = await _billingService.ListByCustomerAsync(customerId);
            return Ok(result);
        }

        /// <summary>
        /// Lista de facturas por usuario
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("ByUser/{userId}")]
        public async Task<IActionResult> ListByUserAsync(int userId)
        {
            var result = await _billingService.ListByUserAsync(userId);
            return Ok(result);
        }

        /// <summary>
        /// Lista de facturas entre un rango de fechas
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        [HttpGet("ByRange/{from}/{to}")]
        public async Task<IActionResult> ListByRangeAsync(DateTime from, DateTime to)
        {
            var result = await _billingService.ListByRangeAsync(from, to);
            return Ok(result);
        }

        /// <summary>
        /// Datos de una factura
        /// </summary>
        /// <param name="billingId"></param>
        /// <returns></returns>
        [HttpGet("{billingId}")]
        public async Task<IActionResult> OneAsync(int billingId)
        {
            var result = await _billingService.OneAsync(billingId);
            return Ok(result);
        }

        /// <summary>
        /// Agregar una nueva factura
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> InsertAsync(BillingModel model)
        {
            var result = await _billingService.InsertAsync(model);
            return Ok(result);
        }

        /// <summary>
        /// Actualizar los datos de una factura
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(BillingModel model)
        {
            var result = await _billingService.UpdateAsync(model);
            return Ok(result);
        }

        /// <summary>
        /// Eliminar una factura
        /// </summary>
        /// <param name="billingId"></param>
        /// <returns></returns>
        [HttpDelete("{billingId}")]
        public async Task<IActionResult> DeleteAsync(int billingId)
        {
            var result = await _billingService.DeleteAsync(billingId);
            return Ok(result);
        }

        /// <summary>
        /// Agregar un producto a la factura
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Details")]
        public async Task<IActionResult> InsertDetailAsync(BillingDetailsModel model)
        {
            var result = await _billingService.InsertDetailAsync(model);
            return Ok(result);
        }

        /// <summary>
        /// Actualizar un producto de la factura
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("Details")]
        public async Task<IActionResult> UpdateDetailAsync(BillingDetailsModel model)
        {
            var result = await _billingService.UpdateDetailAsync(model);
            return Ok(result);
        }

        /// <summary>
        /// Eliminar un producto de la factura
        /// </summary>
        /// <param name="billingDetailId"></param>
        /// <returns></returns>
        [HttpDelete("Detailes/{billingDetailId}")]
        public async Task<IActionResult> DeleteDetailAsync(int billingDetailId)
        {
            var result = await _billingService.DeleteDetailAsync(billingDetailId);
            return  Ok(result);
        }
    }
}
