using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIENDA.Core;
using TIENDA.Data.Entities;
using TIENDA.Data.SqlServer;
using TIENDA.Models;

namespace TIENDA.Data.Services
{
    public class BillingService : IBillingService
    {
        private readonly DBConnection _context;

        public BillingService(DBConnection context)
        {
            _context = context;
        }

        #region Data
        public async Task<TypedResult<List<BillingModel>>> ListByCustomerAsync(int customerId)
        {
            var result = new TypedResult<List<BillingModel>>();

            try
            {
                var model = await _context.Billing
                    .Where(x => x.CustomerId == customerId)
                    .Select(x => new BillingModel
                    {
                        Id = x.Id,
                        CustomerId = x.CustomerId,
                        UserId = x.UserId,
                        Date = x.Date,
                        CustomerName = x.Customer.Name,
                        UserName = x.User == null ? String.Empty : x.User.Name,
                        Created = x.Created,
                        Updated = x.Updated,
                        Details = x.Details.Select(y => new BillingDetailsModel
                        {
                            Id = y.Id,
                            BillingId = y.BillingId,
                            Name = y.Product.Name,
                            ProductId = y.ProductId,
                            Quantity = y.Quantity,
                            UnitPrice = y.UnitPrice,
                        }).ToList(),
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
                result.Message = "Error al cargar la lista de facturas del cliente";
                result.Error = ex;
            }

            return result;
        }
        public async Task<TypedResult<List<BillingModel>>> ListByUserAsync(int userId)
        {
            var result = new TypedResult<List<BillingModel>>();

            try
            {
                var model = await _context.Billing
                    .Where(x => x.UserId == userId)
                    .Select(x => new BillingModel
                    {
                        Id = x.Id,
                        CustomerId = x.CustomerId,
                        UserId = x.UserId,
                        Date = x.Date,
                        CustomerName = x.Customer.Name,
                        UserName = x.User.Name,
                        Created = x.Created,
                        Updated = x.Updated,
                        Details = x.Details.Select(y => new BillingDetailsModel
                        {
                            Id = y.Id,
                            BillingId = y.BillingId,
                            Name = y.Product.Name,
                            ProductId = y.ProductId,
                            Quantity = y.Quantity,
                            UnitPrice = y.UnitPrice,
                        }).ToList(),
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
                result.Message = "Error al cargar la lista de facturas del usuario";
                result.Error = ex;
            }

            return result;
        }
        public async Task<TypedResult<List<BillingModel>>> ListByRangeAsync(DateTime from, DateTime to)
        {
            var result = new TypedResult<List<BillingModel>>();

            try
            {
                var model = await _context.Billing
                    .Where(x => x.Date >= from && x.Date <=to)
                    .Select(x => new BillingModel
                    {
                        Id = x.Id,
                        CustomerId = x.CustomerId,
                        UserId = x.UserId,
                        Date = x.Date,
                        CustomerName = x.Customer.Name,
                        UserName = x.User.Name,
                        Created = x.Created,
                        Updated = x.Updated,
                        Details = x.Details.Select(y => new BillingDetailsModel
                        {
                            Id = y.Id,
                            BillingId = y.BillingId,
                            Name = y.Product.Name,
                            ProductId = y.ProductId,
                            Quantity = y.Quantity,
                            UnitPrice = y.UnitPrice,
                        }).ToList(),
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
                result.Message = "Error al cargar la lista de facturas del usuario";
                result.Error = ex;
            }

            return result;
        }
        public async Task<TypedResult<BillingModel>> OneAsync(int billingId)
        {
            var result = new TypedResult<BillingModel>();

            try
            {
                var model = await _context.Billing
                    .Where(x => x.Id == billingId)
                    .Select(x => new BillingModel
                    {
                        Id = x.Id,
                        CustomerId = x.CustomerId,
                        UserId = x.UserId,
                        Date = x.Date,
                        CustomerName = x.Customer.Name,
                        UserName = x.User.Name,
                        Created = x.Created,
                        Updated = x.Updated,
                        Details = x.Details.Select(y => new BillingDetailsModel
                        {
                            Id = y.Id,
                            BillingId = y.BillingId,
                            Name = y.Product.Name,
                            ProductId = y.ProductId,
                            Quantity = y.Quantity,
                            UnitPrice = y.UnitPrice,
                        }).ToList(),
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
                result.Message = "Error al cargar la lista de facturas del usuario";
                result.Error = ex;
            }

            return result;
        }
        #endregion

        #region Acctions

        public async Task<MsgResult> InsertAsync(BillingModel model)
        {
            var result = new MsgResult();

            var entity = new Bill
            {
                CustomerId = model.CustomerId,
                UserId = model.UserId,
                Date = model.Date,
                Created = DateTime.Now,
                Updated = DateTime.Now,
            };

            _context.Billing.Add(entity);

            result = await _context.SaveAsync();
            if (result.IsSuccess)
            {
                result.Message = "Factura registrada correctamente";
                result.Code = entity.Id;
            }
            else
            {
                result.Message = "Error al registgrar la factura";
            }

            return result;
        }
        public async Task<MsgResult> UpdateAsync(BillingModel model)
        {
            var result = new MsgResult();

            var entity =  await _context.Billing.FirstOrDefaultAsync(x=>x.Id == model.Id);
            if (entity == null)
            {
                result.IsSuccess = false;
                result.Message = "Factura no encontrada";
                result.Code = -1;
                return result;
            }

            entity.Date = model.Date;
            entity.CustomerId = model.CustomerId;
            entity.Updated = DateTime.Now;

            result = await _context.SaveAsync();
            if (result.IsSuccess)
            {
                result.Message = "Factura modificada correctamente";
                result.Code = entity.Id;
            }
            else
            {
                result.Message = "Error al modificar la factura";
            }

            return result;
        }
        public async Task<MsgResult> DeleteAsync(int billingId)
        {
            var result = new MsgResult();

            var entity = await _context.Billing.FirstOrDefaultAsync(x => x.Id == billingId);
            if (entity == null)
            {
                result.IsSuccess = false;
                result.Message = "Factura no encontrada";
                result.Code = -1;
                return result;
            }

            
            _context.Billing.Remove(entity);

            result = await _context.SaveAsync();
            if (result.IsSuccess)
            {
                result.Message = "Factura eliminada correctamente";
                result.Code = entity.Id;
            }
            else
            {
                result.Message = "Error al eliminar la factura";
            }

            return result;
        }
     

        #endregion

        #region DetailActions

        public async Task<MsgResult> InsertDetailAsync(BillingDetailsModel model)
        {
            var result = new MsgResult();

            var billing = await _context.Billing
                .Include(x=>x.Details)
                .FirstOrDefaultAsync(x=>x.Id == model.BillingId);

            if (billing==null)
            {
                result.IsSuccess = false;
                result.Message = "Factura no encontrada";
                result.Code = -1;
                return result;
            }

            billing.Details.Add(new BillingDetail
            {
                ProductId = model.ProductId,
                Quantity = model.Quantity,
                UnitPrice = model.UnitPrice,
            });

            result = await _context.SaveAsync();
            if (result.IsSuccess)
            {
                result.Message = "Producto agregado correctamente a la factura";
            }
            else
            {
                result.Message = "Error al agregar el prodcuto a la factura";
            }

            return result;
        }
        public async Task<MsgResult> UpdateDetailAsync(BillingDetailsModel model)
        {
            var result = new MsgResult();

            var billing = await _context.Billing
                .Include(x => x.Details)
                .FirstOrDefaultAsync(x => x.Id == model.BillingId);

            if (billing == null)
            {
                result.IsSuccess = false;
                result.Message = "Factura no encontrada";
                result.Code = -1;
                return result;
            }

            
            var entity = billing.Details.FirstOrDefault(x=>x.Id == model.Id);

            entity.Quantity = model.Quantity;
            entity.UnitPrice = model.UnitPrice;
            entity.ProductId = model.ProductId;

            result = await _context.SaveAsync();
            if (result.IsSuccess)
            {
                result.Message = "Producto agregado correctamente a la factura";
            }
            else
            {
                result.Message = "Error al agregar el prodcuto a la factura";
            }

            return result;
        }
        public async Task<MsgResult> DeleteDetailAsync(int billingDetailId)
        {
            var result = new MsgResult();

            var entity = await _context.BillingDetails
                .FirstOrDefaultAsync(x => x.Id ==  billingDetailId);

            if (entity == null)
            {
                result.IsSuccess = false;
                result.Message = "Prodcuto no encontrado en la factura";
                result.Code = -1;
                return result;
            }

            _context.BillingDetails.Remove(entity);

            result = await _context.SaveAsync();
            if (result.IsSuccess)
            {
                result.Message = "Producto eliminado correctamente de la factura";
            }
            else
            {
                result.Message = "Error al eliminar el prodcuto de la factura";
            }

            return result;
        }
        #endregion

    }
}
