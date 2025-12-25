using LoyaltyConsole.MVC.ApiResponseMessages;
using LoyaltyConsole.MVC.Services.Interfaces;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace LoyaltyConsole.MVC.Services.Implementations
{
    public class CrudService : ICrudService
    {
        private readonly HttpClient _client;
        private readonly IHttpContextAccessor _context;

        public CrudService(HttpClient client, IHttpContextAccessor context)
        {
            _client = client;
            _context = context;

            var token = _context.HttpContext?.Request.Cookies["token"];
            if (!string.IsNullOrEmpty(token))
            {
                _client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<T> GetAsync<T>(string endpoint)
        {
            var response = await _client.GetAsync(endpoint);

            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(
                    $"API Error | Status: {(int)response.StatusCode} | Body: {content}"
                );
            }

            var apiResponse =
                await response.Content.ReadFromJsonAsync<ApiResponseMessage<T>>();

            return apiResponse.Data;
        }

        public async Task CreateAsync<T>(string endpoint, T entity)
        {
            var response = await _client.PostAsJsonAsync(endpoint, entity);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception(error);
            }
        }

        public async Task CreateWithImageAsync<T>(string endpoint, T entity) where T : class
        {
            var form = new MultipartFormDataContent();

            foreach (var prop in typeof(T).GetProperties())
            {
                var value = prop.GetValue(entity);
                if (value == null) continue;

                if (value is IFormFile file)
                {
                    var stream = file.OpenReadStream();
                    var fileContent = new StreamContent(stream);
                    fileContent.Headers.ContentType =
                        new MediaTypeHeaderValue(file.ContentType);

                    form.Add(fileContent, prop.Name, file.FileName);
                }
                else
                {
                    form.Add(new StringContent(value.ToString()), prop.Name);
                }
            }

            var response = await _client.PostAsync(endpoint, form);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Create failed");
        }

        public async Task UpdateWithImageAsync<T>(string endpoint, int id, T entity) where T : class
        {
            var form = new MultipartFormDataContent();

            foreach (var prop in typeof(T).GetProperties())
            {
                var value = prop.GetValue(entity);
                if (value == null) continue;

                if (value is IFormFile file)
                {
                    var stream = file.OpenReadStream();
                    var fileContent = new StreamContent(stream);
                    fileContent.Headers.ContentType =
                        new MediaTypeHeaderValue(file.ContentType);

                    form.Add(fileContent, prop.Name, file.FileName);
                }
                else
                {
                    form.Add(new StringContent(value.ToString()), prop.Name);
                }
            }

            var response = await _client.PutAsync($"{endpoint}/{id}", form);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Update failed: {error}");
            }
        }


        public async Task UpdateAsync<T>(string endpoint, T entity)
        {
            var response = await _client.PutAsJsonAsync(endpoint, entity);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Update failed");
        }

        public async Task DeleteAsync(string endpoint)
        {
            var response = await _client.DeleteAsync(endpoint);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Delete failed");
        }
    }
}