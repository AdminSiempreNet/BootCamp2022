using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Formats.Asn1;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using TIENDA.Core;
using TIENDA.Models;

namespace Tienda.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Cliente Api");
            var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            HttpClient client = new HttpClient();
            var apiKey = "pUNdbRyotpU6jQ5519T79WVlaG9mHgc1";
            string query = "simpson";
            client.BaseAddress = new Uri($"https://api.giphy.com/");
            var response = await client.GetAsync($"v1/gifs/search?api_key={apiKey}&q={query}&limit=2&offset=0&rating=g&lang=en");
            var result = await response.Content.ReadAsStringAsync();

            Console.WriteLine(result);

            //ApiClient http = new ApiClient();

            ////Autenticar para pedir el token
            //string email = "eduin123@gmail.com";
            //string password = "789654";

            //UserLoginModel loginModel = new UserLoginModel();

            //loginModel.Email = email;
            //loginModel.Password = password;

            //var response = await http.Client.PostAsJsonAsync("api/Users/Login", loginModel);

            //if (!response.IsSuccessStatusCode)
            //{
            //    Console.Write("Error");
            //    return;
            //}

            //var result = JsonSerializer.Deserialize<TypedResult<UserModel>>(await response.Content.ReadAsStringAsync(),
            //    jsonOptions);

            //var token = result.Model.Token;

            ////Autorizamos el cliente
            //http.AddAutorization(token);


            ////Pedir la lista de usuarios
            //var url = "api/Users";
            //var userListResult = await http.Client.GetAsync(url);
                                    

            //var userList = JsonSerializer.Deserialize<TypedResult<List<UserModel>>>(
            //    await userListResult.Content.ReadAsStringAsync(), jsonOptions);

            //foreach (var item in userList.Model)
            //{
            //    Console.WriteLine($"Nombre: {item.Name} | Apellidos: {item.LastName} | Email: {item.Email}");
            //}

            Console.ReadLine();
        }
    }
}
