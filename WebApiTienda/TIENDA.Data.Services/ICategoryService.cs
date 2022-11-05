using System.Collections.Generic;
using System.Threading.Tasks;
using TIENDA.Core;
using TIENDA.Models;

namespace TIENDA.Data.Services
{
    public interface ICategoryService
    {
        Task<TypedResult<List<CategoryModel>>> ListAsync();
        Task<TypedResult<CategoryModel>> OneAsync(int categoryId);
        Task<MsgResult> InsertAsync(CategoryModel model);
        Task<MsgResult> UpdateAsync(CategoryModel model);
        Task<MsgResult> DeleteAsync(int categoryId);
    }
}