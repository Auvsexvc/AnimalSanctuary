using WebApp.Data;
using WebApp.Dtos;
using WebApp.Extensions;
using WebApp.Helpers;
using WebApp.Interfaces;
using WebApp.ViewModels;

namespace WebApp.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly IBaseService _baseService;
        private readonly ILogger<AnimalService> _logger;

        public AnimalService(IBaseService baseService, ILogger<AnimalService> logger)
        {
            _baseService = baseService;
            _logger = logger;
        }

        public async Task<HttpResponseMessage?> CreateAsync(AnimalDto dto, string accessToken)
        {
            return await _baseService.CreateAsync<AnimalDto>(dto, accessToken);
        }

        public async Task<HttpResponseMessage?> DeleteAsync(Guid id, string accessToken)
        {
            return await _baseService.DeleteAsync<Animal>(id, accessToken);
        }

        public async Task<HttpResponseMessage?> EditAsync(Guid id, UpdateAnimalViewModel vm, string accessToken)
        {
            try
            {
                var dto = new AnimalDto()
                {
                    Name = vm.Name,
                    Description = vm.Description,
                    DateOfBirth = vm.DateOfBirth,
                    Sex = vm.Sex,
                    HealthState = vm.HealthState,
                    Attitude = vm.Attitude,
                    DateCreated = vm.DateCreated,
                    SpecieId = vm.SpecieId,
                    FacilityId = vm.FacilityId
                };

                return await _baseService.EditAsync<AnimalDto>(id, dto, accessToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(Message.ERROR, ex.Message);

                return null;
            }
        }

        public async Task<IEnumerable<AnimalViewModel?>?> GetAllAsync(string? sortingField, string? sortingOrder, string? filteringString)
        {
            try
            {
                var data = await _baseService.GetAllAsync<Animal>();

                if (data == null)
                {
                    return Enumerable.Empty<AnimalViewModel>();
                }

                var vms = (await Task.WhenAll(data.Select(obj => Task.Run(async () => await ToViewModel(obj))))).ToList();

                var result = vms.FilterBy(filteringString).SortBy(sortingField, sortingOrder).ToList();

                if (result == null)
                {
                    return Enumerable.Empty<AnimalViewModel>();
                }

                return result.Where(x => x is not null);
            }
            catch (Exception ex)
            {
                _logger.LogError(Message.ERROR, ex.Message);

                return null;
            }
        }

        public async Task<AnimalViewModel?> GetByIdAsync(Guid id)
        {
            try
            {
                var obj = await _baseService.GetByIdAsync<Animal>(id);
                if (obj == null)
                {
                    return null;
                }

                AnimalViewModel? data = await ToViewModel(obj);

                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(Message.ERROR, ex.Message);

                return null;
            }
        }

        public async Task<UpdateAnimalViewModel?> GetByIdUpdateModelAsync(Guid id)
        {
            try
            {
                var obj = await _baseService.GetByIdAsync<Animal>(id);
                if (obj == null)
                {
                    return null;
                }

                UpdateAnimalViewModel? data = await ToUpdateViewModel(obj);

                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(Message.ERROR, ex.Message);

                return null;
            }
        }

        public async Task<NewAnimalDropdownsVM> GetNewUserDropdownsVM()
        {
            return new NewAnimalDropdownsVM()
            {
                Species = (await _baseService.GetAllAsync<AnimalSpecie>(null, null, null))?.OrderBy(a => a.Name).ToList(),
                Facilities = (await _baseService.GetAllAsync<Facility>(null, null, null))?.OrderBy(a => a.Name).ToList(),
                Types = (await _baseService.GetAllAsync<Data.AnimalType>(null, null, null))?.OrderBy(a => a.Name).ToList(),
            };
        }

        public SortingDropdownsViewModel GetSortingDropdownsVM()
        {
            return _baseService.GetSortingDropdownsVM(new AnimalViewModel());
        }

        private async Task<UpdateAnimalViewModel?> ToUpdateViewModel(Animal obj)
        {
            var specie = await _baseService.GetByIdAsync<AnimalSpecie>(obj.SpecieId);
            var type = specie != null ? await _baseService.GetByIdAsync<AnimalType>(specie.TypeId) : null;
            var facility = await _baseService.GetByIdAsync<Facility>(obj.FacilityId);

            if (specie == null || type == null || facility == null)
            {
                return null;
            }

            return new UpdateAnimalViewModel()
            {
                Id = obj.Id,
                Name = obj.Name,
                Description = obj.Description,
                DateOfBirth = obj.DateOfBirth,
                Sex = obj.Sex,
                HealthState = obj.HealthState,
                Attitude = obj.Attitude,
                DateCreated = obj.DateCreated,
                Specie = specie,
                Type = type,
                TypeId = type.Id,
                Facility = facility,
                SpecieId = obj.SpecieId,
                FacilityId = obj.FacilityId,
            };
        }

        private async Task<AnimalViewModel?> ToViewModel(Animal obj)
        {
            var specie = await _baseService.GetByIdAsync<AnimalSpecie>(obj.SpecieId);
            var type = specie != null ? await _baseService.GetByIdAsync<AnimalType>(specie.TypeId) : null;
            var facility = await _baseService.GetByIdAsync<Facility>(obj.FacilityId);

            if (specie == null || type == null || facility == null)
            {
                return null;
            }

            return new AnimalViewModel()
            {
                Id = obj.Id,
                Name = obj.Name,
                Description = obj.Description,
                DateOfBirth = obj.DateOfBirth,
                Sex = obj.Sex,
                HealthState = obj.HealthState,
                Attitude = obj.Attitude,
                DateCreated = obj.DateCreated,
                Specie = specie.Name,
                Type = type.Name,
                Facility = facility.Name
            };
        }
    }
}