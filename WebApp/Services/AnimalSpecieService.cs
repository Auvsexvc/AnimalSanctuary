using WebApp.Data;
using WebApp.Dtos;
using WebApp.Extensions;
using WebApp.Helpers;
using WebApp.Interfaces;
using WebApp.ViewModels;

namespace WebApp.Services
{
    public sealed class AnimalSpecieService : IAnimalSpecieService
    {
        private readonly IBaseService _baseService;
        private readonly ILogger<AnimalSpecieService> _logger;

        public AnimalSpecieService(IBaseService baseService, ILogger<AnimalSpecieService> logger)
        {
            _baseService = baseService;
            _logger = logger;
        }

        public async Task<HttpResponseMessage?> CreateAsync(AnimalSpecieDto dto, string accessToken)
        {
            return await _baseService.CreateAsync<AnimalSpecieDto>(dto, accessToken);
        }

        public async Task<HttpResponseMessage?> DeleteAsync(Guid id, string accessToken)
        {
            return await _baseService.DeleteAsync<AnimalSpecie>(id, accessToken);
        }

        public async Task<HttpResponseMessage?> EditAsync(Guid id, UpdateSpecieViewModel vm, string accessToken)
        {
            try
            {
                var dto = new AnimalSpecieDto()
                {
                    Name = vm.Name,
                    Description = vm.Description,
                    TypeId = vm.TypeId
                };

                return await _baseService.EditAsync<AnimalSpecieDto>(id, dto, accessToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(Message.ERROR, ex.Message);

                return null;
            }
        }

        public async Task<IEnumerable<AnimalSpecieViewModel?>?> GetAllAsync(string? sortingField, string? sortingOrder, string? filteringString)
        {
            try
            {
                var data = await _baseService.GetAllAsync<AnimalSpecie>();

                if (data == null)
                {
                    return Enumerable.Empty<AnimalSpecieViewModel>();
                }

                var vms = (await Task.WhenAll(data.Select(obj => Task.Run(async () => await ToViewModel(obj))))).ToList();

                var result = vms.FilterBy(filteringString).SortBy(sortingField, sortingOrder).ToList();

                if (result == null)
                {
                    return Enumerable.Empty<AnimalSpecieViewModel>();
                }

                return result.Where(x => x is not null);
            }
            catch (Exception ex)
            {
                _logger.LogError(Message.ERROR, ex.Message);

                return null;
            }
        }

        public async Task<AnimalSpecieViewModel?> GetByIdAsync(Guid id)
        {
            try
            {
                var obj = await _baseService.GetByIdAsync<AnimalSpecie>(id);
                if (obj == null)
                {
                    return null;
                }

                AnimalSpecieViewModel? data = await ToViewModel(obj);

                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(Message.ERROR, ex.Message);

                return null;
            }
        }

        public async Task<NewSpecieDropdownsVM> GetNewSpecieDropdownsVMAsync()
        {
            return new NewSpecieDropdownsVM()
            {
                Types = (await _baseService.GetAllAsync<AnimalType>(null, null, null))?.OrderBy(a => a.Name).ToList()
            };
        }

        public SortingDropdownsViewModel GetSortingDropdownsVM()
        {
            return _baseService.GetSortingDropdownsVM(new AnimalSpecieViewModel());
        }

        public async Task<UpdateSpecieViewModel?> GetByIdUpdateModelAsync(Guid id)
        {
            try
            {
                var obj = await _baseService.GetByIdAsync<AnimalSpecie>(id);
                if (obj == null)
                {
                    return null;
                }

                UpdateSpecieViewModel? data = await ToUpdateViewModel(obj);

                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(Message.ERROR, ex.Message);

                return null;
            }
        }

        private async Task<AnimalSpecieViewModel?> ToViewModel(AnimalSpecie obj)
        {
            var type = await _baseService.GetByIdAsync<AnimalType>(obj.TypeId);

            if (type == null)
            {
                return null;
            }

            return new AnimalSpecieViewModel()
            {
                Id = obj.Id,
                Name = obj.Name,
                Description = obj.Description,
                TypeName = type.Name
            };
        }

        private async Task<UpdateSpecieViewModel?> ToUpdateViewModel(AnimalSpecie obj)
        {
            var type = await _baseService.GetByIdAsync<AnimalType>(obj.TypeId);

            if (type == null)
            {
                return null;
            }

            return new UpdateSpecieViewModel()
            {
                Id = obj.Id,
                Name = obj.Name,
                Description = obj.Description,
                TypeId = obj.TypeId,
                Type = type
            };
        }
    }
}