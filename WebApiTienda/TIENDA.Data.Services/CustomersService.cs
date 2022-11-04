using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIENDA.Core;
using TIENDA.Data.Entities;
using TIENDA.Data.SqlServer;
using TIENDA.Models;

namespace TIENDA.Data.Services
{
    public class CustomersService
    {
        private readonly DBConnection _context;

        public CustomersService(DBConnection context)
        {
            _context = context;
        }

        public async Task<TypedResult<List<CustomerModel>>> ListTopAsync(int top = 10)
        {
            var result = new TypedResult<List<CustomerModel>>();

            try
            {
                var model = await _context.Customers
                    .Select(x => new CustomerModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        PhoneNumber = x.PhoneNumber,
                        Email = x.Email,
                        CityId = x.CityId,
                        CityName = x.City.Name,
                        BillingCount = x.Bills.Count,

                        //LastBuy = x.Bills.LastOrDefault() == null ? null : x.Bills.LastOrDefault().Date,

                        Created = x.Created,
                        Updated = x.Updated,

                    })
                    .OrderByDescending(x=>x.BillingCount)
                    .Take(top)
                    .ToListAsync();

                result.IsSuccess = true;
                result.Message = "OK";
                result.Count = model.Count;
                result.Code = 1;
                result.Model = model;

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = "Error al cargar el cliente especificado";
                result.Error = ex;

            }

            return result;
        }

        public async Task<TypedResult<List<CustomerModel>>> ListAsync(int cityId)
        {
            var result = new TypedResult<List<CustomerModel>>();

            try
            {
                var model = await _context.Customers
                    .Where(x=>x.CityId == cityId)
                    .Select(x => new CustomerModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        PhoneNumber = x.PhoneNumber,
                        Email = x.Email,
                        CityId = x.CityId,
                        CityName = x.City.Name,
                        BillingCount = x.Bills.Count,

                        //LastBuy = x.Bills.LastOrDefault() == null ? null : x.Bills.LastOrDefault().Date,

                        Created = x.Created,
                        Updated = x.Updated,

                    }).ToListAsync();

                result.IsSuccess = true;
                result.Message = "OK";
                result.Count = model.Count;
                result.Code = 1;
                result.Model = model;

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = "Error al cargar el cliente especificado";
                result.Error = ex;

            }

            return result;
        }

        public async Task<TypedResult<List<CustomerModel>>> ListAsync()
        {
            var result = new TypedResult<List<CustomerModel>>();

            try
            {
                var model = await _context.Customers
                    .Select(x => new CustomerModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        PhoneNumber = x.PhoneNumber,
                        Email = x.Email,
                        CityId = x.CityId,
                        CityName = x.City.Name,
                        BillingCount = x.Bills.Count,

                        //LastBuy = x.Bills.LastOrDefault() == null ? null : x.Bills.LastOrDefault().Date,

                        Created = x.Created,
                        Updated = x.Updated,

                    }).ToListAsync();

                result.IsSuccess = true;
                result.Message = "OK";
                result.Count = model.Count;
                result.Code = 1;
                result.Model = model;

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = "Error al cargar el cliente especificado";
                result.Error = ex;

            }

            return result;
        }

        public async Task<TypedResult<CustomerModel>> OneAsync(int customerId)
        {
            var result = new TypedResult<CustomerModel>();

            try
            {
                var model = await _context.Customers
                    .Where(x=>x.Id == customerId)
                    .Select(x => new CustomerModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        PhoneNumber = x.PhoneNumber,
                        Email = x.Email,
                        CityId = x.CityId,
                        CityName =  x.City.Name,
                        BillingCount = x.Bills.Count,

                        //LastBuy = x.Bills.LastOrDefault() == null ? null : x.Bills.LastOrDefault().Date,

                        Created = x.Created,
                        Updated = x.Updated,

                    }).ToListAsync();

                result.IsSuccess = true;
                result.Message = "OK";
                result.Count = model.Count;
                result.Code = 1;
                result.Model = model.FirstOrDefault();

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = "Error al cargar el cliente especificado";
                result.Error = ex;

            }

            return result;
        }

        public async Task<MsgResult> InsertAsync(CustomerPostModel model)
        {
            var result = new MsgResult();
                       

            var entity = new Customer
            {
                Name = model.Name,
                CityId = model.CityId,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,

                Created = DateTime.Now,
                Updated = DateTime.Now,
            };

            _context.Customers.Add(entity);

            result = await _context.SaveAsync();

            if (result.IsSuccess)
            {
                result.Message = "Cliente registrado correctamente";
                result.Code = entity.Id;
            }
            else
            {
                result.Message = "Error al registrar el cliente";
            }

            return result;
        }

        public async Task<MsgResult> UpdateAsync(CustomerPostModel model)
        {
            var result = new MsgResult();
                        
            var entity = await _context.Customers
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (entity == null)
            {
                result.IsSuccess = false;
                result.Message = "Registro no encontrado";
                return result;
            }

            entity.Name = model.Name;
            entity.CityId = model.CityId;
            entity.PhoneNumber = model.PhoneNumber;
            entity.Email = model.Email;
            entity.Updated = DateTime.Now;

            result = await _context.SaveAsync();

            if (result.IsSuccess)
            {
                result.Message = "Cliente modificado correctamente";
                result.Code = entity.Id;
            }
            else
            {
                result.Message = "Error al modifcar el cliente";
            }
            return result;
        }

        public async Task<MsgResult> DeleteAsync(int customerId)
        {
            var result = new MsgResult();


            var entity = await _context.Customers
                .FirstOrDefaultAsync(x => x.Id == customerId);

            if (entity == null)
            {
                result.IsSuccess = false;
                result.Message = "Registro no encontrado";
                result.Code = -1;
                return result;
            }

            //Validar datos relacionados
            var count = await _context.Billing
                .Where(x=>x.CustomerId == customerId).CountAsync();

            if (count > 0)
            {
                result.IsSuccess = false;
                result.Message = "No se puede eliminar el cliente porque tiene facturas registradas. " +
                    "Elimine primero las facturas asociadas al cliente";
                result.Code = -2;
                return result;
            }


            _context.Customers.Remove(entity);

            result = await _context.SaveAsync();

            if (result.IsSuccess)
            {
                result.Message = "Cliente eliminado correctamente";
                result.Code = entity.Id;
            }
            else
            {
                result.Message = "Error al eliminar el cliente";
            }

            return result;
        }
    }
}
