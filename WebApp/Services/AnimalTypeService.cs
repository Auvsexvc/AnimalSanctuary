using WebClientApp.Data;
using WebClientApp.Dtos;
using WebClientApp.Extensions;
using WebClientApp.Helpers;
using WebClientApp.Interfaces;
using WebClientApp.ViewModels;

namespace WebClientApp.Services
{
    public sealed class AnimalTypeService : ITypeService
    {
        private readonly IBaseService _baseService;
        private readonly ILogger<AnimalTypeService> _logger;

        public AnimalTypeService(IBaseService baseService, ILogger<AnimalTypeService> logger)
        {
            _baseService = baseService;
            _logger = logger;
        }

        public async Task<HttpResponseMessage?> CreateAsync(AnimalTypeDto dto, string accessToken)
        {
            return await _baseService.CreateAsync(dto, accessToken);
        }

        public async Task<HttpResponseMessage?> DeleteAsync(Guid id, string accessToken)
        {
            return await _baseService.DeleteAsync<AnimalType>(id, accessToken);
        }

        public async Task<HttpResponseMessage?> EditAsync(Guid id, AnimalTypeViewModel vm, string accessToken)
        {
            try
            {
                var dto = new AnimalTypeDto()
                {
                    Name = vm.Name,
                    Description = vm.Description
                };

                return await _baseService.EditAsync(id, dto, accessToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(Message.ERROR, ex.Message);

                return null;
            }
        }

        public async Task<IEnumerable<AnimalTypeViewModel?>?> GetAllAsync(string? sortingField, string? sortingOrder, string? filteringString)
        {
            try
            {
                var data = await _baseService.GetAllAsync<AnimalType>();

                if (data == null)
                {
                    return Enumerable.Empty<AnimalTypeViewModel>();
                }

                var vms = data.Select(obj => ToViewModel(obj));

                var result = vms.FilterBy(filteringString).SortBy(sortingField, sortingOrder).ToList();

                if (result == null)
                {
                    return Enumerable.Empty<AnimalTypeViewModel>();
                }

                return result.Where(x => x is not null);
            }
            catch (Exception ex)
            {
                _logger.LogError(Message.ERROR, ex.Message);

                return null;
            }
        }

        public async Task<AnimalTypeViewModel?> GetByIdAsync(Guid id)
        {
            try
            {
                var obj = await _baseService.GetByIdAsync<AnimalType>(id);
                if (obj == null)
                {
                    return null;
                }

                AnimalTypeViewModel? data = ToViewModel(obj);

                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(Message.ERROR, ex.Message);

                return null;
            }
        }

        public SortingDropdownsViewModel GetSortingDropdownsVM()
        {
            return _baseService.GetSortingDropdownsVM(new AnimalTypeViewModel());
        }

        private static AnimalTypeViewModel? ToViewModel(AnimalType obj)
        {
            if (obj == null)
            {
                return null;
            }

            return new AnimalTypeViewModel()
            {
                Id = obj.Id,
                Name = obj.Name,
                Description = obj.Description,
            };
        }
    }
}