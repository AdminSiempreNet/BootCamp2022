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

        [HttpGet("ByUser/{userId}")]
        public async Task<IActionResult> ListByUserAsync(int userId)
        {
            var result = await _billingService.ListByUserAsync(userId);
            return Ok(result);
        }

        [HttpGet("ByRange/{from}/{to}")]
        public async Task<IActionResult> ListByRangeAsync(DateTime from, DateTime to)
        {
            var result = await _billingService.ListByRangeAsync(from, to);
            return Ok(result);
        }


        [HttpGet("{billingId}")]
        public async Task<IActionResult> OneAsync(int billingId)
        {
            var result = await _billingService.OneAsync(billingId);
            return Ok(result);
        }

        //Crear, modificar y eliminar facturas
        [HttpPost]
        public async Task<IActionResult> InsertAsync(BillingModel model)
        {
            var result = await _billingService.InsertAsync(model);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(BillingModel model)
        {
            var result = await _billingService.UpdateAsync(model);
            return Ok(result);
        }

        [HttpDelete("{billingId}")]
        public async Task<IActionResult> DeleteAsync(int billingId)
        {
            var result = await _billingService.DeleteAsync(billingId);
            return Ok(result);
        }

        //Crear, modificar y eliminar detalles de la factura
        [HttpPost("Details")]
        public async Task<IActionResult> InsertDetailAsync(BillingDetailsModel model)
        {
            var result = await _billingService.InsertDetailAsync(model);
            return Ok(result);
        }

        [HttpPut("Details")]
        public async Task<IActionResult> UpdateDetailAsync(BillingDetailsModel model)
        {
            var result = await _billingService.UpdateDetailAsync(model);
            return Ok(result);
        }


        [HttpDelete("Detailes/{billingDetailId}")]
        public async Task<IActionResult> DeleteDetailAsync(int billingDetailId)
        {
            var result = await _billingService.DeleteDetailAsync(billingDetailId);
            return  Ok(result);
        }
    }
}
