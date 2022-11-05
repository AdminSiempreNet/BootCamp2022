using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TIENDA.Core;
using TIENDA.Data.Entities;
using TIENDA.Data.SqlServer;
using TIENDA.Models;

namespace TIENDA.Data.Services
{
    public class CitiesService : ICitiesService
    {
        DBConnection _context;
        public CitiesService(DBConnection context)
        {
            _context = context;
        }

        public async Task<TypedResult<List<CityModel>>> ListAsync()
        {

            var result = new TypedResult<List<CityModel>>();

            try
            {
                var model = await _context.Cities
                .Select(city => new CityModel
                {
                    Id = city.Id,
                    Name = city.Name,
                    CustomerCount = city.Customers.Count,
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
                result.Message = "Error al cargar la lista de ciudades";
                result.Error = ex;
            }

            return result;

        }

        public async Task<TypedResult<CityModel>> OneAsync(int cityId)
        {
            var result = new TypedResult<CityModel>();

            try
            {
                var model = await _context.Cities
                    .Select(city => new CityModel
                    {
                        Id = city.Id,
                        Name = city.Name,
                        CustomerCount = city.Customers.Count,
                    })
                    .Where(x => x.Id == cityId).ToListAsync();

                result.IsSuccess = true;
                result.Message = "OK";
                result.Count = model.Count;
                result.Code = 1;
                result.Model = model.FirstOrDefault();
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = "Error al cargar la ciudad especificada";
                result.Error = ex;
            }

            return result;
        }

        public async Task<TypedResult<CityModel>> InsertAsync(CityModel model)
        {
            var result = new TypedResult<CityModel>();

            var entity = new City
            {
                Name = model.Name,
            };

            _context.Cities.Add(entity);

            var res = await _context.SaveAsync();
            if (res.IsSuccess)
            {
                result.IsSuccess = true;
                result.Message = "OK";
                result.Code = 1;

                model.Id = entity.Id;
                result.Model = model;
            }
            else
            {
                result.IsSuccess = false;
                result.Message = "Error al registrar la ciudad";
                result.Error = res.Error;
            }

            return result;
        }

        public async Task<TypedResult<CityModel>> UpdateAsync(CityModel model)
        {
            var result = new TypedResult<CityModel>();

            var entity = await _context.Cities.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (entity == null)
            {
                result.IsSuccess = false;
                result.Message = "Ciudad no encontrada";
                return result;
            }

            entity.Name = model.Name;

            var res = await _context.SaveAsync();
            if (res.IsSuccess)
            {
                result.IsSuccess = true;
                result.Message = "Ciudad modificada correctamente";
                result.Code = 1;
                result.Model = model;
            }
            else
            {
                result.IsSuccess = false;
                result.Message = "Error al guardar los cambios en la ciudad";
                result.Error = res.Error;
            }

            return result;
        }

        public async Task<MsgResult> DeleteAsync(int cityId)
        {
            var result = new MsgResult();

            var entity = await _context.Cities
                .Include(x => x.Customers)
                .FirstOrDefaultAsync(x => x.Id == cityId);

            if (entity == null)
            {
                result.IsSuccess = false;
                result.Message = "Ciudad no encontrada";
                result.Code = -1;
                return result;
            }

            //Validamos los datos relacionados
            if (entity.Customers.Count > 0)
            {
                result.IsSuccess = false;
                result.Message = "No se puede eliminar la ciudad porque tiene clientes relacionados";
                result.Code = -2;
                return result;
            }

            _context.Cities.Remove(entity);

            var res = await _context.SaveAsync();
            if (res.IsSuccess)
            {
                result.IsSuccess = true;
                result.Message = "Ciudad eliminada correctamente";
                result.Code = 1;
            }
            else
            {
                result.IsSuccess = false;
                result.Message = "Error al eliminar la ciudad especificada";
                result.Error = res.Error;
            }

            return result;
        }
    }
}
