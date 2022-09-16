using WebApp.Data;
using WebApp.Dtos;
using WebApp.Helpers;
using WebApp.Interfaces;
using WebApp.ViewModels;

namespace WebApp.Services
{
    public class AnimalsService : IAnimalsService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AnimalsService> _logger;

        public AnimalsService(IConfiguration configuration, ILogger<AnimalsService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<IEnumerable<T>?> GetAllAsync<T>(string? sortingField, string? sortingOrder, string? filteringString)
        {
            try
            {
                IEnumerable<T> data = Enumerable.Empty<T>();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration.GetConnectionString("DefaultConnection"));
                    var result = await client.GetAsync($"{typeof(T).Name}?sortingField={sortingField}&sortingOrder={sortingOrder}&filteringString={filteringString}");

                    if (result.IsSuccessStatusCode)
                    {
                        var json = await result.Content.ReadAsStringAsync();

                        if (!string.IsNullOrEmpty(json))
                        {
                            return (await result.Content.ReadFromJsonAsync<IList<T>>())!;
                        }
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

        public async Task<Animal?> GetByIdAsync(Guid? id)
        {
            try
            {
                Animal? data = null;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration.GetConnectionString("DefaultConnection"));
                    var result = await client.GetAsync($"Animal/{id}");

                    if (result.IsSuccessStatusCode)
                    {
                        data = await result.Content.ReadFromJsonAsync<Animal>();
                    }
                }

                return data!;
            }
            catch (Exception ex)
            {
                _logger.LogError(Message.ERROR, ex.Message);

                return null;
            }
        }

        public async Task<HttpResponseMessage?> CreateAsync(AnimalDto dto)
        {
            try
            {
                using var client = new HttpClient();
                client.BaseAddress = new Uri(_configuration.GetConnectionString("DefaultConnection"));

                var result = await client.PostAsJsonAsync<AnimalDto>("Animal", dto);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(Message.ERROR, ex.Message);

                return null;
            }
        }

        public async Task<HttpResponseMessage?> EditAsync(Guid id, AnimalDto dto)
        {
            try
            {
                using var client = new HttpClient();
                client.BaseAddress = new Uri(_configuration.GetConnectionString("DefaultConnection"));

                var result = await client.PutAsJsonAsync<AnimalDto>($"Animal/{id}", dto);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(Message.ERROR, ex.Message);

                return null;
            }
        }

        public async Task<HttpResponseMessage?> DeleteAsync(Guid? id)
        {
            try
            {
                using var client = new HttpClient();
                client.BaseAddress = new Uri(_configuration.GetConnectionString("DefaultConnection"));

                var result = await client.DeleteAsync($"Animal/{id}");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(Message.ERROR, ex.Message);

                return null;
            }
        }

        public async Task<NewAnimalDropdownsVM> GetNewAnimalDropdownsVM()
        {
            return new NewAnimalDropdownsVM()
            {
                Species = (await GetAllAsync<AnimalSpecie>(null, null, null))?.OrderBy(a => a.Name).ToList(),
                Facilities = (await GetAllAsync<Facility>(null, null, null))?.OrderBy(a => a.Name).ToList(),
            };
        }
    }
}