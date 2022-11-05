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
    public class CategoryService : ICategoryService
    {
        private readonly DBConnection _context;

        public CategoryService(DBConnection context)
        {
            _context = context;
        }

        public async Task<TypedResult<List<CategoryModel>>> ListAsync()
        {
            var result = new TypedResult<List<CategoryModel>>();

            try
            {
                var model = await _context.Categories
                    .Select(x => new CategoryModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        ProductCount = x.Products.Count
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
                result.Message = "Error al cargar la lista de categorías";
                result.Error = ex;

            }

            return result;
        }

        public async Task<TypedResult<CategoryModel>> OneAsync(int categoryId)
        {
            var result = new TypedResult<CategoryModel>();

            try
            {
                var model = await _context.Categories
                    .Where(x => x.Id == categoryId)
                    .Select(x => new CategoryModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        ProductCount = x.Products.Count
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
                result.Message = "Error al cargar la categoría especificada";
                result.Error = ex;

            }

            return result;
        }

        public async Task<MsgResult> InsertAsync(CategoryModel model)
        {
            var result = new MsgResult();

            //Validamos si ya existe una categoría con ese nombre
            var entity = await _context.Categories
                .FirstOrDefaultAsync(x => x.Name == model.Name);

            if (entity != null)
            {
                result.IsSuccess = false;
                result.Message = $"Ya existe una categoría con el nombre {model.Name}";
                return result;
            }

            entity = new Category
            {
                Name = model.Name,
            };

            _context.Categories.Add(entity);

            result = await _context.SaveAsync();

            if (result.IsSuccess)
            {
                result.Message = "Categoría agregada correctamente";
                result.Code = entity.Id;
            }
            else
            {
                result.Message = "Error al agregar la categoría";
            }

            return result;
        }

        public async Task<MsgResult> UpdateAsync(CategoryModel model)
        {
            var result = new MsgResult();

            var entity = await _context.Categories
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (entity == null)
            {
                result.IsSuccess = false;
                result.Message = "Registro no encontrado";
                return result;
            }

            entity.Name = model.Name;

            result = await _context.SaveAsync();

            if (result.IsSuccess)
            {
                result.Message = "Categoría modificada correctamente";
                result.Code = entity.Id;
            }
            else
            {
                result.Message = "Error al modifcar la categoría";
            }
            return result;
        }

        public async Task<MsgResult> DeleteAsync(int categoryId)
        {
            var result = new MsgResult();


            var entity = await _context.Categories
                .FirstOrDefaultAsync(x => x.Id == categoryId);

            if (entity == null)
            {
                result.IsSuccess = false;
                result.Message = "Registro no encontrado";
                result.Code = -1;
                return result;
            }

            //Validar datos relacionados
            var categoryProducts = await _context.Products
                .Where(x => x.CategoryId == categoryId).CountAsync();

            if (categoryProducts > 0)
            {
                result.IsSuccess = false;
                result.Message = "No se puede eliminar la categoría porque tiene productos registrados. " +
                    "Elimine primero los productos de la categoría";
                result.Code = -2;
                return result;
            }


            _context.Categories.Remove(entity);

            result = await _context.SaveAsync();

            if (result.IsSuccess)
            {
                result.Message = "Categoría eliminada correctamente";
                result.Code = entity.Id;
            }
            else
            {
                result.Message = "Error al eliminar la categoría";
            }

            return result;
        }
    }
}
