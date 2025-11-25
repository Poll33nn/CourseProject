using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
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
        /// <summary>
        /// Получает список участков с их информацией о них.
        /// </summary>
        /// <returns>List<ForestPlotDto> информаии об участках.</returns>
        public async Task<HttpStatusCode> Login(string login, string password)
        {
            var response = await App.HttpClient.PostAsJsonAsync("Account",
                new LoginDto
                {
                    Login = login,
                    PasswordHash = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(password))),
                });

            if (response.IsSuccessStatusCode)
            {
                var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponseDto>();

                App.UserFullName = loginResponse.UserFullName;
                App.UserRole = loginResponse.UserRole;

                App.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResponse.Token);
            }
            
            return response.StatusCode;
        }
        /// <summary>
        /// Получает список участков с их информацией о них.
        /// </summary>
        /// <returns>List<ForestPlotDto> информаии об участках.</returns>
        public async Task<List<ForestPlotDto>> GetForestPlotsAsync()
        {
            var response = await App.HttpClient.GetAsync("ForestPlots");//"ForestPlots"

            if (response.IsSuccessStatusCode)
            {
                var forestPlots = await response.Content.ReadFromJsonAsync<List<ForestPlotDto>>();
                return forestPlots;
            }

            return null;
        }
        public async Task<HttpStatusCode> CreateForestPlotAsync(CreateForestPlotDto forestPlotDto)
        {
            var forestPlotJson = new StringContent(
                JsonSerializer.Serialize(forestPlotDto)
                , Encoding.UTF8
                , "application/json");

            var response = await App.HttpClient.PostAsync("ForestPlots", forestPlotJson);

            if (response.IsSuccessStatusCode)
            {
                return HttpStatusCode.Created;
            }

            return response.StatusCode;
        }
        public async Task<HttpStatusCode> UpdateForestPlot(UpdateForestPlotDto forestPlotDto)
        {
            var forestPlotJson = new StringContent(
                JsonSerializer.Serialize(forestPlotDto)
                , Encoding.UTF8
                , "application/json");

            var response = await App.HttpClient.PutAsync($"ForestPlots/{forestPlotDto.PlotId}", forestPlotJson);
            if (response.IsSuccessStatusCode)
            {
                return HttpStatusCode.OK;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
            }

            return response.StatusCode;
        }
        public async Task<HttpStatusCode> DeleteForestPlotAsync(int PlotId)
        {
            var response = await App.HttpClient.DeleteAsync($"ForestPlots/{PlotId}");

            if (response.IsSuccessStatusCode)
            {
                return HttpStatusCode.OK;
            }

            return response.StatusCode;
        }
        public async Task<List<UserDto>> GetAllResponsibleAsync()
        {
            var response = await App.HttpClient.GetAsync("Users");

            if (response.IsSuccessStatusCode)
            {
                var users = await response.Content.ReadFromJsonAsync<List<UserDto>>();
                return users;
            }

            return null;
        }
        public async Task<List<TreeTypeDto>> GetTreeTypeNameAsync()
        {
            var response = await App.HttpClient.GetAsync("TreeTypes");

            if (response.IsSuccessStatusCode)
            {
                var treeTypes = await response.Content.ReadFromJsonAsync<List<TreeTypeDto>>();
                return treeTypes;
            }

            return null;
        }
        public async Task<List<EventTypeDto>> GetEventTypeNameAsync()
        {
            var response = await App.HttpClient.GetAsync("EventTypes");

            if(response.IsSuccessStatusCode)
            {
                var eventName = await response.Content.ReadFromJsonAsync<List<EventTypeDto>>();
                return eventName;
            }

            return null;
        }
        public async Task<List<ForestEventReportDto>> GetAllSilvicultureEventAsync()
        {
            var response = await App.HttpClient.GetAsync("SilvicultureEvents");

            if (response.IsSuccessStatusCode)
            {
                var forestryEvents = await response.Content.ReadFromJsonAsync<List<ForestEventReportDto>>();
                return forestryEvents;
            }

            return null;
        }
        public async Task<List<ForestEventReportDto>> GetPlotSilvicultureEventAsync(int PlotId)
        {
            var response = await App.HttpClient.GetAsync($"SilvicultureEvents/{PlotId}");

            if (response.IsSuccessStatusCode)
            {
                var forestryEvents = await response.Content.ReadFromJsonAsync<List<ForestEventReportDto>>();
                return forestryEvents;
            }

            return null;
        }
        public async Task<HttpStatusCode> CreateSilvicultureEventAsync(CreateSilvicultureEventDto silvicultureEvent)
        {
            var silvicultureEventJson = new StringContent(
                JsonSerializer.Serialize(silvicultureEvent)
                , Encoding.UTF8
                , "application/json");

            var response = await App.HttpClient.PostAsync("SilvicultureEvents", silvicultureEventJson);

            if (response.IsSuccessStatusCode)
            {
                return HttpStatusCode.Created;
            }

            return response.StatusCode;
        }
        public async Task<HttpStatusCode> CreateUserAsync(CreateUserDto createUserDto)
        {
            var userJson = new StringContent(
                JsonSerializer.Serialize(createUserDto)
                , Encoding.UTF8
                , "application/json");

            var response = await App.HttpClient.PostAsync("Users", userJson);

            if (response.IsSuccessStatusCode)
            {
                return HttpStatusCode.Created;
            }

            return response.StatusCode;
        }
        public async Task<HttpStatusCode> DeleteUserAsync(int userId)
        {
            var response = await App.HttpClient.DeleteAsync($"Users/{userId}");

            if (response.IsSuccessStatusCode)
            {
                return HttpStatusCode.OK;
            }
            var errorContent = await response.Content.ReadAsStringAsync();

            // Выводим ошибку в окно отладки
            Debug.WriteLine($"Failed to delete user ID {userId}. Status: {response.StatusCode}. Response: {errorContent}");

            return response.StatusCode;
        }
        public async Task<List<RoleDto>> GetRoleNameAsync()
        {
            var response = await App.HttpClient.GetAsync("Roles");

            if (response.IsSuccessStatusCode)
            {
                var roleName = await response.Content.ReadFromJsonAsync<List<RoleDto>>();
                return roleName;
            }

            return null;
        }
    }
}
