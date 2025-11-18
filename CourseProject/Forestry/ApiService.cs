using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using ServiceLayer.DTO_s;
using ServiceLayer.Models;

namespace Forestry
{
    public class ApiService
    {

        public async Task<HttpStatusCode> Login(string login, string password)
        {
            var response = await App.HttpClient.PostAsJsonAsync("Account",
                new LoginDto
                {
                    Login = login,
                    PasswordHash = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(password))),
                });
            var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponseDto>();

            App.UserFullName = loginResponse.UserFullName;
            App.UserRole = loginResponse.UserRole;

            App.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResponse.Token);
            return response.StatusCode;
        }

        public async Task<List<ForestPlotDto>> GetForestPlotsAsync()
        {
            var response = await App.HttpClient.GetAsync("ForestPlots");

            if (response.IsSuccessStatusCode)
            {
                var forestPlots = await response.Content.ReadFromJsonAsync<List<ForestPlotDto>>();
                return forestPlots;
            }

            return null;
        }
    }
}
