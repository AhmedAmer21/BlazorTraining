using Blazor.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Text;

namespace BlazorDemo.Sevices
{
    public class UserDataService : IUserDataService
    {
        private readonly HttpClient httpClient;

        public UserDataService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<User>>(await httpClient.GetStreamAsync($"api/User")
                , new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        }
        public async Task<User> AddUser(User user)
        {
            var employeeJson = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"api/User", employeeJson);
            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<User>(await response.Content.ReadAsStreamAsync());
            }
            return null;
        }
        //public async Task<User> GetUser(long userId)
        //{
        //    var response = await httpClient.GetAsync($"api/User/{userId}");
        //    return await JsonSerializer.DeserializeAsync<User>(await response.Content.ReadAsStreamAsync());
        //}
        public async Task<User> GetUser(long userId)
        {
            return await JsonSerializer.DeserializeAsync<User>(await httpClient.GetStreamAsync($"api/User/{userId}"), new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });
        }
        public async Task DeleteUser(long userId)
        {
            await httpClient.DeleteAsync($"api/User/{userId}");
        }
    }
}
