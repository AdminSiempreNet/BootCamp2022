using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIENDA.Core;
using TIENDA.Models;

namespace TIENDA.Data.Services
{
    public interface IProductService
    {
        Task<TypedResult<ProductModel>> OneAsync(int productId);
        Task<TypedResult<List<ProductModel>>> ListAsync(int? categoryId = null, string findExpression = null);

        Task<MsgResult> InsertAsync(ProductModel model);
        Task<MsgResult> UpdateAsync(ProductModel model);
        Task<MsgResult> DeleteAsync(int productId);
    }
}
