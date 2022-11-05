using System.Collections.Generic;
using System.Threading.Tasks;
using TIENDA.Core;
using TIENDA.Models;

namespace TIENDA.Data.Services
{
    public interface ICitiesService
    {
        Task<TypedResult<List<CityModel>>> ListAsync();
        Task<TypedResult<CityModel>> OneAsync(int cityId);
        Task<TypedResult<CityModel>> InsertAsync(CityModel model);
        Task<TypedResult<CityModel>> UpdateAsync(CityModel model);
        Task<MsgResult> DeleteAsync(int cityId);
    }
}