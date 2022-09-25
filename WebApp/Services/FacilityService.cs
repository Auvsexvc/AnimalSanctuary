using WebApp.Data;
using WebApp.Dtos;
using WebApp.Helpers;
using WebApp.Interfaces;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Services
{
    public class FacilityService : IFacilityService
    {
        private readonly IBaseService _baseService;
        private readonly ILogger<FacilityService> _logger;

        public FacilityService(IBaseService baseService, ILogger<FacilityService> logger)
        {
            _baseService = baseService;
            _logger = logger;
        }

        public async Task<HttpResponseMessage?> CreateAsync(FacilityDto dto, string accessToken)
        {
            return await _baseService.CreateAsync<FacilityDto>(dto, accessToken);
        }

        public async Task<HttpResponseMessage?> DeleteAsync(Guid id, string accessToken)
        {
            var data = await GetByIdAsync(id);

            if (data?.Animals?.Any() == true)
            {
                return null;
            }

            return await _baseService.DeleteAsync<Facility>(id, accessToken);
        }

        public async Task<HttpResponseMessage?> EditAsync(Guid id, UpdateFacilityViewModel vm, string accessToken)
        {
            try
            {
                var dto = new FacilityDto()
                {
                    Name = vm.Name,
                    Description = vm.Description,
                    BuildingNumber = vm.BuildingNumber,
                    ApartmentNumber = vm.ApartmentNumber,
                    StreetName = vm.StreetName,
                    PhoneNumber = vm.PhoneNumber,
                    City = vm.City,
                    MaxCapacity = vm.MaxCapacity
                };

                return await _baseService.EditAsync<FacilityDto>(id, dto, accessToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(Message.ERROR, ex.Message);

                return null;
            }
        }

        public async Task<IEnumerable<FacilityViewModel?>?> GetAllAsync(string? sortingField, string? sortingOrder, string? filteringString)
        {
            try
            {
                var data = await _baseService.GetAllAsync<Facility>(sortingField, sortingOrder, filteringString);

                if (data == null)
                {
                    return Enumerable.Empty<FacilityViewModel>();
                }

                var result = (await Task.WhenAll(data.Select(obj => Task.Run(async () => await ToViewModel(obj))))).ToList();

                if (result == null)
                {
                    return Enumerable.Empty<FacilityViewModel>();
                }

                return result.Where(x => x is not null);
            }
            catch (Exception ex)
            {
                _logger.LogError(Message.ERROR, ex.Message);

                return null;
            }
        }

        public async Task<FacilityViewModel?> GetByIdAsync(Guid id)
        {
            try
            {
                var obj = await _baseService.GetByIdAsync<Facility>(id);
                if (obj == null)
                {
                    return null;
                }

                FacilityViewModel? data = await ToViewModel(obj);

                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(Message.ERROR, ex.Message);

                return null;
            }
        }

        public async Task<NewFacilityDropdownsVM> GetNewFacilityDropdownsVM()
        {
            return new NewFacilityDropdownsVM()
            {
                Animals = (await _baseService.GetAllAsync<Animal>(null, null, null))?.OrderBy(a => a.Name).ToList()
            };
        }

        public SortingDropdowns GetSortingDropdownsVM()
        {
            return _baseService.GetSortingDropdownsVM(new FacilityViewModel());
        }

        public async Task<UpdateFacilityViewModel?> GetByIdUpdateModelAsync(Guid id)
        {
            try
            {
                var obj = await _baseService.GetByIdAsync<Facility>(id);
                if (obj == null)
                {
                    return null;
                }

                UpdateFacilityViewModel? data = await ToUpdateViewModel(obj);

                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(Message.ERROR, ex.Message);

                return null;
            }
        }

        private async Task<FacilityViewModel?> ToViewModel(Facility obj)
        {
            var animals = await _baseService.GetAllAsync<Animal>(null, null, obj.Id.ToString());

            if (animals == null)
            {
                return null;
            }

            return new FacilityViewModel()
            {
                Id = obj.Id,
                Name = obj.Name,
                Description = obj.Description,
                BuildingNumber = obj.BuildingNumber,
                ApartmentNumber = obj.ApartmentNumber,
                StreetName = obj.StreetName,
                City = obj.City!,
                PhoneNumber = obj.PhoneNumber,
                MaxCapacity = obj.MaxCapacity,
                FreeSpace = obj.FreeSpace,
                Animals = animals
                //Animals = string.Join(", ", animals.Select(x => x.Name))
            };
        }

        private async Task<UpdateFacilityViewModel?> ToUpdateViewModel(Facility obj)
        {
            var animals = await _baseService.GetAllAsync<Animal>(null, null, obj.Id.ToString());

            if (animals == null)
            {
                return null;
            }

            return new UpdateFacilityViewModel()
            {
                Id = obj.Id,
                Name = obj.Name,
                Description = obj.Description,
                BuildingNumber = obj.BuildingNumber,
                ApartmentNumber = obj.ApartmentNumber,
                StreetName = obj.StreetName,
                City = obj.City!,
                PhoneNumber = obj.PhoneNumber,
                MaxCapacity = obj.MaxCapacity,
                FreeSpace = obj.FreeSpace,
                AnimalsIds = obj.Animals,
                Animals = animals
            };
        }
    }
}