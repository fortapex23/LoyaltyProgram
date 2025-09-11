using LoyaltyConsole.MVC.ApiResponseMessages;
using LoyaltyConsole.MVC.Services.Interfaces;
using RestSharp;

namespace LoyaltyConsole.MVC.Services.Implementations
{
    public class CrudService : ICrudService
    {
        private readonly RestClient _restClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CrudService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _restClient = new RestClient(_configuration.GetSection("Api:URL").Value);
            var token = _httpContextAccessor.HttpContext.Request.Cookies["token"];

            if (token != null)
            {
                _restClient.AddDefaultHeader("Authorization", "Bearer " + token);
            }
        }

        public async Task Create<T>(string endpoint, T entity) where T : class
        {
            var request = new RestRequest(endpoint, Method.Post);
            request.AddJsonBody(entity);

            var response = await _restClient.ExecuteAsync<ApiResponseMessage<T>>(request);

            if (!response.IsSuccessful)
            {
                var errorMessage = $"Error: {response.StatusCode} Content: {response.Content}";
                throw new Exception(errorMessage);
            }
        }

        public async Task CreateWithImage<T>(string endpoint, T entity, IFormFile file = null) where T : class
        {
            var request = new RestRequest(endpoint, Method.Post);

            if (file != null)
            {
                request.AlwaysMultipartFormData = true;

                // Add normal properties
                foreach (var prop in entity.GetType().GetProperties())
                {
                    var value = prop.GetValue(entity);
                    if (value != null && !(value is IFormFile))
                        request.AddParameter(prop.Name, value.ToString());
                }

                // Add file as byte[]
                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    var fileBytes = ms.ToArray();
                    request.AddFile("Image", fileBytes, file.FileName, file.ContentType);
                }
            }
            else
            {
                request.AddJsonBody(entity);
            }

            var response = await _restClient.ExecuteAsync<ApiResponseMessage<T>>(request);

            if (!response.IsSuccessful)
            {
                var errorMessage = $"Error: {response.StatusCode} Content: {response.Content}";
                throw new Exception(errorMessage);
            }
        }

        public async Task Delete<T>(string endpoint, int id)
        {
            var request = new RestRequest(endpoint, Method.Delete);

            var response = await _restClient.ExecuteAsync<ApiResponseMessage<T>>(request);

            if (!response.IsSuccessful)
            {
                var errorMessage = $"Error: {response.StatusCode}. Content: {response.Content}";
                throw new Exception(errorMessage);
            }
        }

        public async Task DeleteItem<T>(string endpoint)
        {
            var request = new RestRequest(endpoint, Method.Delete);

            var response = await _restClient.ExecuteAsync<ApiResponseMessage<T>>(request);
            if (!response.IsSuccessful)
            {
                var errorMessage = $"Error: {response.StatusCode}. Content: {response.Content}";
                throw new Exception(errorMessage);
            }
        }

        public async Task<T> GetAllAsync<T>(string endpoint)
        {
            var request = new RestRequest(endpoint, Method.Get);
            var response = await _restClient.ExecuteAsync<ApiResponseMessage<T>>(request);

            if (!response.IsSuccessful)
            {
                var errorMessage = $"Error: {response.StatusCode}. Content: {response.Content}";
                throw new Exception(errorMessage);
            }

            return response.Data.Data;
        }

        public async Task<T> GetByStringIdAsync<T>(string endpoint, string? id)
        {
            var request = new RestRequest(endpoint, Method.Get);
            var response = await _restClient.ExecuteAsync<ApiResponseMessage<T>>(request);

            if (!response.IsSuccessful)
            {
                string errorMessage = "An error occurred";

                if (response.Data != null && !string.IsNullOrEmpty(response.Data.ErrorMessage))
                {
                    errorMessage = response.Data.ErrorMessage;
                }
                else if (!string.IsNullOrEmpty(response.ErrorMessage))
                {
                    errorMessage = response.ErrorMessage;
                }
                else
                {
                    errorMessage = response.StatusDescription ?? response.StatusCode.ToString();
                }

                throw new Exception(errorMessage);
            }

            return response.Data.Data;
        }


        public async Task<T> GetByIdAsync<T>(string endpoint, int? id)
        {
            if (id < 1) throw new Exception();
            var request = new RestRequest(endpoint, Method.Get);
            var response = await _restClient.ExecuteAsync<ApiResponseMessage<T>>(request);

            if (!response.IsSuccessful)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    throw new Exception(response.Data.ErrorMessage);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new Exception(response.Data.ErrorMessage);
                }
            }

            return response.Data.Data;
        }


        public async Task Update<T>(string endpoint, T entity) where T : class
        {
            var request = new RestRequest(endpoint, Method.Put);
            request.AddJsonBody(entity);

            var response = await _restClient.ExecuteAsync<ApiResponseMessage<T>>(request);

            if (!response.IsSuccessful)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest && response.Data.PropertyName is not null)
                {
                    throw new Exception(response.Data.ErrorMessage);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    throw new Exception(response.Data.ErrorMessage);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new Exception(response.Data.ErrorMessage);
                }
            }
        }

        public async Task<bool> IsExist(string endpoint, int? id)
        {
            if (id < 1) throw new ArgumentException("Invalid Id");

            var request = new RestRequest($"{endpoint}/{id}", Method.Get);
            var response = await _restClient.ExecuteAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return false;
            }
            else
            {
                throw new Exception($"Error Status code: {response.StatusCode} Message: {response.Content}");
            }
        }
    }
}
