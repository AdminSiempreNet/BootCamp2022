using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIENDA.Core;
using TIENDA.Models;

namespace TIENDA.Data.Services
{
    public interface IUserService
    {
        Task<TypedResult<List<UserModel>>> ListAsync();
        Task<TypedResult<UserModel>> OneAsync(int userId);

        Task<MsgResult> InsertAsyc(UserRegisterModel model);
        Task<MsgResult> UpdateAsync(UserModel model);
        Task<MsgResult> DeleteAsync(int userId);

        Task<TypedResult<UserModel>> Login(UserLoginModel model);
        Task<MsgResult> RecoverPassword(RecoverPasswordModel model); //TODO => Resolver problema
        Task<MsgResult> ChangePassword(ChangePasswordModel model);
    }
}
