﻿using WebApp.Data;
using WebApp.Dtos;
using WebApp.Helpers;
using WebApp.Interfaces;
using WebApp.Models;
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

        public async Task<HttpResponseMessage?> CreateAsync(AnimalDto dto)
        {
            return await _baseService.CreateAsync<AnimalDto>(dto);
        }

        public async Task<HttpResponseMessage?> DeleteAsync(Guid id)
        {
            return await _baseService.DeleteAsync<Animal>(id);
        }

        public async Task<HttpResponseMessage?> EditAsync(Guid id, AnimalViewModel vm)
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

                return await _baseService.EditAsync<AnimalDto>(id, dto);
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
                var data = await _baseService.GetAllAsync<Animal>(sortingField, sortingOrder, filteringString);

                if (data == null)
                {
                    return Enumerable.Empty<AnimalViewModel>();
                }

                var result = (await Task.WhenAll(data.Select(obj => Task.Run(async () => await ToViewModel(obj))))).ToList();

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

        public async Task<NewAnimalDropdownsVM> GetNewAnimalDropdownsVM()
        {
            return new NewAnimalDropdownsVM()
            {
                Species = (await _baseService.GetAllAsync<AnimalSpecie>(null, null, null))?.OrderBy(a => a.Name).ToList(),
                Facilities = (await _baseService.GetAllAsync<Facility>(null, null, null))?.OrderBy(a => a.Name).ToList(),
                Types = (await _baseService.GetAllAsync<AnimalType>(null, null, null))?.OrderBy(a => a.Name).ToList(),
            };
        }

        public AnimalSortingDropdowns GetAnimalSortingDropdownsVM()
        {
            return new AnimalSortingDropdowns()
            {
                Fields = new FilterAnimalViewModel().GetType().GetProperties().Select(p => p.Name).ToList(),
                Order = new List<string>() { "asc", "desc" }
            };
        }

        private async Task<AnimalViewModel?> ToViewModel(Animal obj)
        {
            var specie = await _baseService.GetByIdAsync<AnimalSpecie>(obj.SpecieId);
            var type = await _baseService.GetByIdAsync<AnimalType>(obj.TypeId);
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
                Specie = specie,
                Type = type,
                Facility = facility,
                SpecieId = obj.SpecieId,
                FacilityId = obj.FacilityId,
                TypeId = obj.TypeId
            };
        }
    }
}