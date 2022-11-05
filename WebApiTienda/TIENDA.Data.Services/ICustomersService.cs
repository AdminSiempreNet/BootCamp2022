using System.Collections.Generic;
using System.Threading.Tasks;
using TIENDA.Core;
using TIENDA.Models;

namespace TIENDA.Data.Services
{
    public interface ICustomersService
    {
        Task<TypedResult<List<CustomerModel>>> ListAsync();
        Task<TypedResult<List<CustomerModel>>> ListAsync(int cityId);
        Task<TypedResult<List<CustomerModel>>> ListTopAsync(int top = 10);
        Task<TypedResult<CustomerModel>> OneAsync(int customerId);
        Task<MsgResult> InsertAsync(CustomerPostModel model);
        Task<MsgResult> UpdateAsync(CustomerPostModel model);
        Task<MsgResult> DeleteAsync(int customerId);
    }
}