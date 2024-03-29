﻿using WebClientApp.Data;
using WebClientApp.Dtos;
using WebClientApp.Extensions;
using WebClientApp.Helpers;
using WebClientApp.Interfaces;
using WebClientApp.ViewModels;

namespace WebClientApp.Services
{
    public sealed class AnimalService : IAnimalService
    {
        private readonly IBaseService _baseService;
        private readonly IImageService _imageService;
        private readonly ILogger<AnimalService> _logger;

        public AnimalService(IBaseService baseService, ILogger<AnimalService> logger, IImageService imageService)
        {
            _baseService = baseService;
            _logger = logger;
            _imageService = imageService;
        }

        public async Task<HttpResponseMessage?> CreateAsync(AnimalDto dto, string accessToken)
        {
            var result = await _baseService.CreateAsync(dto, accessToken);

            if (result == null)
            {
                return null;
            }

            if (result.Headers.Location == null)
            {
                return null;
            }

            var guidOfCreated = result.Headers.Location.Segments.LastOrDefault();
            if (guidOfCreated != null && dto.ProfileImg != null)
            {
                await _imageService.UploadImageAsync(dto.ProfileImg, Guid.Parse(guidOfCreated), accessToken);
            }

            return result;
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
                    FacilityId = vm.FacilityId,
                    ProfileImg = vm.ProfileImg
                };

                if (dto.ProfileImg != null)
                {
                    await _imageService.UploadImageAsync(dto.ProfileImg, id, accessToken);
                }

                return await _baseService.EditAsync(id, dto, accessToken);
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

        public async Task<NewAnimalDropdownsVM> GetNewUserDropdownsVMAsync()
        {
            return new NewAnimalDropdownsVM()
            {
                Species = (await _baseService.GetAllAsync<AnimalSpecie>(null, null, null))?.OrderBy(a => a.Name).ToList(),
                Facilities = (await _baseService.GetAllAsync<Facility>(null, null, null))?.OrderBy(a => a.Name).ToList(),
                Types = (await _baseService.GetAllAsync<AnimalType>(null, null, null))?.OrderBy(a => a.Name).ToList(),
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
            var image = await _baseService.GetByIdAsync<Image>(obj.Id);

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
                ProfileImgPath = image == null ? _baseService.ApiUri + "../Images/Default.png" : _baseService.ApiUri + ".." + image.Path
            };
        }

        private async Task<AnimalViewModel?> ToViewModel(Animal obj)
        {
            var specie = await _baseService.GetByIdAsync<AnimalSpecie>(obj.SpecieId);
            var type = specie != null ? await _baseService.GetByIdAsync<AnimalType>(specie.TypeId) : null;
            var facility = await _baseService.GetByIdAsync<Facility>(obj.FacilityId);
            var image = await _baseService.GetByIdAsync<Image>(obj.Id);

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
                Facility = facility.Name,
                ProfileImgPath = image == null ? _baseService.ApiUri + "../Images/Default.png" : _baseService.ApiUri + ".." + image.Path
            };
        }
    }
}