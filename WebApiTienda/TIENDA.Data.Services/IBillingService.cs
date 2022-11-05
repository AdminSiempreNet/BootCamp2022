using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIENDA.Core;
using TIENDA.Models;

namespace TIENDA.Data.Services
{
    public interface IBillingService
    {
        //Retorno de datos
        Task<TypedResult<List<BillingModel>>> ListByCustomerAsync(int customerId);
        Task<TypedResult<List<BillingModel>>> ListByUserAsync(int userId);
        Task<TypedResult<List<BillingModel>>> ListByRangeAsync(DateTime from, DateTime to);
        Task<TypedResult<BillingModel>> OneAsync(int  billingId);


        //Crear, modificar y eliminar facturas
        Task<MsgResult> InsertAsync(BillingModel model);
        Task<MsgResult> UpdateAsync(BillingModel model);
        Task<MsgResult> DeleteAsync(int billingId);
        Task<MsgResult> DeleteListAsync(List<int> billingId);


        //Crear, modificar y eliminar detalles de la factura
        Task<MsgResult> InsertDetailAsync(BillingDetailsModel model);
        Task<MsgResult> UpdateDetailAsync(BillingDetailsModel model);
        Task<MsgResult> DeleteDetailAsync(int billingDetailId);
    }
}
