using System.ComponentModel;
using System.Net.Http.Headers;
using WebClientApp.Dtos;
using WebClientApp.Extensions;
using WebClientApp.Helpers;
using WebClientApp.Interfaces;
using WebClientApp.ViewModels;

namespace WebClientApp.Services
{
    public class BaseService : IBaseService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<BaseService> _logger;

        public string ApiUri { get; }

        public BaseService(ILogger<BaseService> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
            ApiUri = _httpClient.BaseAddress!.AbsoluteUri;
        }

        public async Task<IEnumerable<T>?> GetAllAsync<T>(string? sortingField, string? sortingOrder, string? filteringString)
        {
            try
            {
                IEnumerable<T> data = Enumerable.Empty<T>();

                using var result = await _httpClient.GetAsync($"{typeof(T).Name}?sortingField={sortingField}&sortingOrder={sortingOrder}&filteringString={filteringString}");

                if (result.IsSuccessStatusCode)
                {
                    var json = await result.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(json))
                    {
                        return (await result.Content.ReadFromJsonAsync<IList<T>>())!;
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(Message.ERROR, ex.Message);

                return null;
            }
        }

        public async Task<IEnumerable<T>?> GetAllAsync<T>()
        {
            try
            {
                IEnumerable<T> data = Enumerable.Empty<T>();

                using var result = await _httpClient.GetAsync($"{typeof(T).Name}");

                if (result.IsSuccessStatusCode)
                {
                    var json = await result.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(json))
                    {
                        return (await result.Content.ReadFromJsonAsync<IList<T>>())!;
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(Message.ERROR, ex.Message);

                return null;
            }
        }

        public async Task<T?> GetByIdAsync<T>(Guid id)
        {
            try
            {
                T? data = default;

                using var result = await _httpClient.GetAsync($"{typeof(T).Name}/{id}");

                if (result.IsSuccessStatusCode)
                {
                    data = await result.Content.ReadFromJsonAsync<T>();
                }

                return data!;
            }
            catch (Exception ex)
            {
                _logger.LogError(Message.ERROR, ex.Message);

                return default;
            }
        }

        public async Task<HttpResponseMessage?> CreateAsync<T>(T dto, string accessToken)
        {
            try
            {
                var name = string.Concat(typeof(T).Name.TakeLast(3)) == "Dto" ? string.Concat(typeof(T).Name.SkipLast(3)) : typeof(T).Name;

                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                var result = await _httpClient.PostAsJsonAsync($"{name}", dto);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(Message.ERROR, ex.Message);

                return null;
            }
        }

        public async Task<HttpResponseMessage?> PostImageAsync(ImageDto dto, string accessToken)
        {
            try
            {
                using var multipartFormContent = new MultipartFormDataContent();
                var file = dto.Image;

                using var fileStream = file!.OpenReadStream();
                var fileStreamContent = new StreamContent(fileStream);
                fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/*");

                multipartFormContent.Add(fileStreamContent, name: file.Name, fileName: file.FileName);

                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                var result = await _httpClient.PostAsync($"Image/{dto.ContextId}", multipartFormContent);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(Message.ERROR, ex.Message);

                return null;
            }
        }

        public async Task<HttpResponseMessage?> EditAsync<T>(Guid id, T dto, string accessToken)
        {
            try
            {
                var name = string.Concat(typeof(T).Name.TakeLast(3)) == "Dto" ? string.Concat(typeof(T).Name.SkipLast(3)) : typeof(T).Name;

                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                var result = await _httpClient.PutAsJsonAsync($"{name}/{id}", dto);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(Message.ERROR, ex.Message);

                return null;
            }
        }

        public async Task<HttpResponseMessage?> DeleteAsync<T>(Guid id, string accessToken)
        {
            try
            {
                var name = string.Concat(typeof(T).Name.TakeLast(3)) == "Dto" ? string.Concat(typeof(T).Name.SkipLast(3)) : typeof(T).Name;

                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                var result = await _httpClient.DeleteAsync($"{name}/{id}");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(Message.ERROR, ex.Message);

                return null;
            }
        }

        public SortingDropdownsViewModel GetSortingDropdownsVM<T>(T obj)
        {
            var fields = obj!
                .GetType()
                .GetProperties()
                .Where(p => Attribute.IsDefined(p, typeof(DisplayNameAttribute)))
                .Select(p => p.Name)
                .ToList();

            var displayNames = obj!
                .GetType()
                .GetProperties()
                .Where(p => Attribute.IsDefined(p, typeof(DisplayNameAttribute)))
                .ToDictionary(p => p.Name, p => p.GetAttribute<DisplayNameAttribute>(false) != null
                    ? p.GetAttribute<DisplayNameAttribute>(false)!.DisplayName
                    : p.Name);

            var order = new List<string>() { "asc", "desc" };

            return new SortingDropdownsViewModel()
            {
                Fields = fields,
                DisplayNames = displayNames,
                Order = order
            };
        }
    }
}