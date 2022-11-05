using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIENDA.Core;
using TIENDA.Models;

namespace TIENDA.Data.Services
{
    public class BillingService : IBillingService
    {

        #region Data
        public Task<TypedResult<List<BillingModel>>> ListByCustomerAsync(int customerId)
        {
            throw new NotImplementedException();
        }
        public Task<TypedResult<List<BillingModel>>> ListByUserAsync(int userId)
        {
            throw new NotImplementedException();
        }
        public Task<TypedResult<List<BillingModel>>> ListByRangeAsync(DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }
        public Task<TypedResult<BillingModel>> OneAsync(int billingId)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Acctions

        public Task<MsgResult> InsertAsync(BillingModel model)
        {
            throw new NotImplementedException();
        }
        public Task<MsgResult> UpdateAsync(BillingModel model)
        {
            throw new NotImplementedException();
        }
        public Task<MsgResult> DeleteAsync(int billingId)
        {
            throw new NotImplementedException();
        }
        public Task<MsgResult> DeleteListAsync(List<int> billingId)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region DetailActions

        public Task<MsgResult> InsertDetailAsync(BillingDetailsModel model)
        {
            throw new NotImplementedException();
        }
        public Task<MsgResult> UpdateDetailAsync(BillingDetailsModel model)
        {
            throw new NotImplementedException();
        }
        public Task<MsgResult> DeleteDetailAsync(int billingDetailId)
        {
            throw new NotImplementedException();
        } 
        #endregion

    }
}
