using Microsoft.EntityFrameworkCore;
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
    public class ProductService : IProductService
    {
        private readonly DBConnection _context;

        public ProductService(DBConnection context)
        {
            _context = context;
        }

        public async Task<TypedResult<ProductModel>> OneAsync(int productId)
        {
            var result = new TypedResult<ProductModel>();

            try
            {
                var model = await _context.Products
                    .Where(x=>x.Id == productId)
                    .Select(x=> new ProductModel
                    {
                        Id = x.Id,
                        Name = x.Name,  
                        CategoryId = x.CategoryId,
                        CategoryName = x.Category.Name,
                        Stok = x.Stok,
                        Code = x.Code,
                        Cost = x.Cost,
                        Price = x.Price,
                        
                        Created = x.Created,
                        Updated = x.Updated,

                    }).ToListAsync();

                result.IsSuccess = true;
                result.Message = "OK";
                result.Model = model.FirstOrDefault();
                result.Count = model.Count;
                result.Code = 1;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = "Error al cargar los datos del producto";
                result.Error = ex;
            }

            return result;
        }

        public async Task<TypedResult<List<ProductModel>>> ListAsync(int? categoryId = null, 
            string findExpression = null)
        {
            var result = new TypedResult<List<ProductModel>>();

            try
            {

                //TODO: Que hago si no funciona el strin.IsNullOrEmpty

                var model = await _context.Products
                    .Where(x=>x.CategoryId == categoryId || categoryId == null)
                    .Where(x => x.Name.Contains(findExpression) 
                            || string.IsNullOrEmpty(findExpression))
                    .Select(x => new ProductModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        CategoryId = x.CategoryId,
                        CategoryName = x.Category.Name,
                        Stok = x.Stok,
                        Code = x.Code,
                        Cost = x.Cost,
                        Price = x.Price,

                        Created = x.Created,
                        Updated = x.Updated,

                    }).ToListAsync();

                result.IsSuccess = true;
                result.Message = "OK";
                result.Model = model;
                result.Count = model.Count;
                result.Code = 1;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = "Error al cargar la lista de productos";
                result.Error = ex;
            }

            return result;
        }

        public async Task<MsgResult> InsertAsync(ProductModel model)
        {
            var result = new MsgResult();

            var entity = await _context.Products
                .FirstOrDefaultAsync(x=>x.Code == model.Code);

            if (entity!=null)
            {
                result.IsSuccess = false;
                //result.Message = $"Ya existe un producto con el código {model.Code}";
                result.Message = $"El código {model.Code} ya se encuentra asignado al prodcuto {entity.Name}";
                return result;
            }

            entity = new Product
            {
                CategoryId = model.CategoryId,

                Name = model.Name,
                Code = model.Code,
                Cost = model.Cost,
                Price = model.Price,
                Stok = model.Stok,

                Created = DateTime.Now,
                Updated = DateTime.Now,
            };


            _context.Products.Add(entity);

            result = await _context.SaveAsync();
            if (result.IsSuccess)
            {
                result.Message = "Producto registrado correctamente";
                result.Code = entity.Id;
            }
            else
            {
                result.Message = "Error al registrar el producto";
            }

            return result;
        }
        public async Task<MsgResult> UpdateAsync(ProductModel model)
        {
            var result = new MsgResult();

            var entity = await _context.Products
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (entity == null)
            {
                result.IsSuccess = false;
                result.Message = $"Prodcuto no encontrado";                
                return result;
            }

           
            entity.Name = model.Name;
            entity.CategoryId = model.CategoryId;
            entity.Cost = model.Cost;
            entity.Price = model.Price;
            entity.Stok = model.Stok;
            entity.Updated = DateTime.Now;


            result = await _context.SaveAsync();
            if (result.IsSuccess)
            {
                result.Message = "Producto modificado correctamente";
            }
            else
            {
                result.Message = "Error al modificar el producto";
            }

            return result;
        }
        public async Task<MsgResult> DeleteAsync(int productId)
        {
            var result = new MsgResult();

            var entity = await _context.Products
                .FirstOrDefaultAsync(x => x.Id == productId);

            if (entity == null)
            {
                result.IsSuccess = false;
                result.Message = $"Prodcuto no encontrado";
                return result;
            }

            var count = await _context.BillingDetails.Where(x=>x.ProductId == productId).CountAsync();

            if (count > 0 )
            {
                result.IsSuccess = false;
                result.Message = $"No se puede eliminar el prodcuto porque este ya tiene facturas asociadas";
                return result;
            }

            _context.Products.Remove(entity);

            result = await _context.SaveAsync();
            if (result.IsSuccess)
            {
                result.Message = "Producto eliminado correctamente";
            }
            else
            {
                result.Message = "Error al eliminar el producto";
            }

            return result;
        }
    }
}
