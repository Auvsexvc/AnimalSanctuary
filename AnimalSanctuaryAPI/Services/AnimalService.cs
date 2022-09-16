using AnimalSanctuaryAPI.Data;
using AnimalSanctuaryAPI.Dtos;
using AnimalSanctuaryAPI.Extensions;
using AnimalSanctuaryAPI.Helpers;
using AnimalSanctuaryAPI.Interfaces;
using AnimalSanctuaryAPI.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AnimalSanctuaryAPI.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<AnimalService> _logger;

        public AnimalService(AppDbContext appDbContext, ILogger<AnimalService> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<AnimalViewModel>?> GetViewModels(string? sortingField, string? sortingOrder, string? filteringString)
        {
            try
            {
                var data = await _appDbContext.Animals.Include(x => x.Facility).Include(x => x.Specie).ThenInclude(x => x.Type).ToListAsync();
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

        public async Task<AnimalViewModel?> GetViewModel(Guid id)
        {
            try
            {
                var data = await _appDbContext.Animals.Include(x => x.Facility).Include(x => x.Specie).ThenInclude(x => x.Type).ToListAsync();

                if (data == null)
                {
                    return null;
                }

                return data.Find(f => f.Id == id)?.ToViewModel();
            }
            catch (Exception ex)
            {
                _logger.LogError(Message.ERROR, ex.Message);
                return null;
            }
        }

        public async Task<AnimalViewModel?> Add(AnimalDto dto)
        {
            try
            {
                var specie = await _appDbContext.Species.FirstOrDefaultAsync(t => t.Id == dto.SpecieId);
                var facility = await _appDbContext.Facilities.FirstOrDefaultAsync(t => t.Id == dto.FacilityId);

                if (specie == null || facility == null)
                {
                    return null;
                }

                var data = dto.NewFromDto(specie, facility);

                await _appDbContext.Animals.AddAsync(data);
                await _appDbContext.SaveChangesAsync();

                return data.ToViewModel();
            }
            catch (Exception ex)
            {
                _logger.LogError(Message.ERROR, ex.Message);
                return null;
            }
        }

        public async Task<AnimalViewModel?> Update(Guid id, AnimalDto dto)
        {
            try
            {
                var datas = await _appDbContext.Animals.Include(x => x.Specie).Include(x => x.Facility).ToListAsync();

                if (datas == null)
                {
                    return null;
                }

                var data = datas.Find(f => f.Id == id);

                var specie = await _appDbContext.Species.FirstOrDefaultAsync(t => t.Id == dto.SpecieId);
                var facility = await _appDbContext.Facilities.FirstOrDefaultAsync(t => t.Id == dto.FacilityId);

                if (data == null || specie == null || facility == null)
                {
                    return null;
                }

                data = data.UpdateFromDto(dto, specie, facility);

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
                var datas = await _appDbContext.Animals.ToListAsync();

                if (datas == null)
                {
                    return null;
                }

                var data = datas.Find(f => f.Id == id);

                if (data == null)
                {
                    return null;
                }

                _appDbContext.Animals.Remove(data);
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