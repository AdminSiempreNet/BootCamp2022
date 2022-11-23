using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TIENDA.Core;
using TIENDA.Models;
using static System.Net.WebRequestMethods;

namespace Tienda.Client
{
    public class UserApiClient
    {
        private readonly ApiClient _apiClient;

        JsonSerializerOptions jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        public UserApiClient(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<TypedResult<List<UserModel>>> List()
        {
            var url = "api/Users";
            var userListResult = await _apiClient.Client.GetAsync(url);

            if (!userListResult.IsSuccessStatusCode)
            {
                return new TypedResult<List<UserModel>>
                {
                    IsSuccess = false,
                    Message = "Error al leer los datos de la api",
                };
            }

            var userList = JsonSerializer.Deserialize<TypedResult<List<UserModel>>>(
                await userListResult.Content.ReadAsStringAsync(), jsonOptions);

            return userList;
        }

     
    }
}
