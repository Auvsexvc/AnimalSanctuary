using AnimalSanctuaryAPI.Data;
using AnimalSanctuaryAPI.Dtos;
using AnimalSanctuaryAPI.Extensions;
using AnimalSanctuaryAPI.Helpers;
using AnimalSanctuaryAPI.Interfaces;
using AnimalSanctuaryAPI.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AnimalSanctuaryAPI.Services
{
    public class AnimalTypeService : IAnimalTypeService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<AnimalTypeService> _logger;

        public AnimalTypeService(AppDbContext appDbContext, ILogger<AnimalTypeService> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<AnimalTypeViewModel>?> GetViewModels(string? sortingField, string? sortingOrder, string? filteringString)
        {
            try
            {
                var data = await _appDbContext.Types.ToListAsync();
                if (data == null)
                {
                    return null;
                }

                return data.ConvertAll(f => f.ToViewModel()).FilterBy(filteringString).SortBy(sortingField, sortingOrder).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(Message.ERROR, ex.Message);
                return null;
            }
        }

        public async Task<AnimalTypeViewModel?> GetViewModel(Guid id)
        {
            try
            {
                var data = await _appDbContext.Types.ToListAsync();

                if (data == null)
                {
                    return null;
                }

                return data.Find(d => d.Id == id)?.ToViewModel();
            }
            catch (Exception ex)
            {
                _logger.LogError(Message.ERROR, ex.Message);
                return null;
            }
        }

        public async Task<AnimalTypeViewModel?> Add(AnimalTypeDto dto)
        {
            try
            {
                var data = dto.NewFromDto();

                await _appDbContext.Types.AddAsync(data);
                await _appDbContext.SaveChangesAsync();

                return data.ToViewModel();
            }
            catch (Exception ex)
            {
                _logger.LogError(Message.ERROR, ex.Message);
                return null;
            }
        }

        public async Task<AnimalTypeViewModel?> Update(Guid id, AnimalTypeDto dto)
        {
            try
            {
                var datas = await _appDbContext.Types.ToListAsync();

                if (datas == null)
                {
                    return null;
                }

                var data = datas.Find(f => f.Id == id);

                if (data == null)
                {
                    return null;
                }

                data = data.UpdateFromDto(dto);

                _appDbContext.Entry(data).State = EntityState.Modified;

                await _appDbContext.SaveChangesAsync();

                return data.ToViewModel();
            }
            catch (Exception ex)
            {
                _logger.LogError(Message.ERROR, ex.Message);
                return null;
            }
        }

        public async Task<Guid?> Delete(Guid id)
        {
            try
            {
                var datas = await _appDbContext.Types.ToListAsync();

                if (datas == null)
                {
                    return null;
                }

                var data = datas.Find(f => f.Id == id);

                if (data == null)
                {
                    return null;
                }

                _appDbContext.Types.Remove(data);
                await _appDbContext.SaveChangesAsync();

                return id;
            }
            catch (Exception ex)
            {
                _logger.LogError(Message.ERROR, ex.Message);
                return null;
            }
        }
    }
}