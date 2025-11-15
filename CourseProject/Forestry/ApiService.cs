using ServiceLayer.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace Forestry
{
    public class ApiService
    {
        private readonly HttpClient _client;
        public ApiService(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri("http://localhost:5163/api");
        }

        public async Task Login(string login, string password)
        {
            var response = await _client.PostAsJsonAsync("Account",
                new LoginDto
                {
                    Login = login,
                    PasswordHash = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(password))),
                });
            response.EnsureSuccessStatusCode();
            var token = await response.Content.ReadAsStringAsync();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

    }
}
