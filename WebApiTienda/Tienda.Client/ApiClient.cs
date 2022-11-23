using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
namespace Tienda.Client
{
    public class ApiClient
    {
        public HttpClient Client { get; set; }
        private readonly string _urlBase;
        public ApiClient()
        {
            Client = new HttpClient();
            _urlBase = "https://localhost:44340/";
            Client.BaseAddress = new Uri(_urlBase);
        }


        public void AddAutorization(string token)
        {
            Client.DefaultRequestHeaders.Remove("Authorization");
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }

        public void RemoveAutorization(string token)
        {
            Client.DefaultRequestHeaders.Remove("Authorization");
        }
    }
}
