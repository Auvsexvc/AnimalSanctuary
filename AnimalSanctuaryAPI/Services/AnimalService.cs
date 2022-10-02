using AnimalSanctuaryAPI.Data;
using AnimalSanctuaryAPI.Dtos;
using AnimalSanctuaryAPI.Exceptions;
using AnimalSanctuaryAPI.Extensions;
using AnimalSanctuaryAPI.Helpers;
using AnimalSanctuaryAPI.Interfaces;
using AnimalSanctuaryAPI.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AnimalSanctuaryAPI.Services
{
    public sealed class AnimalService : IAnimalService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<AnimalService> _logger;

        public AnimalService(AppDbContext appDbContext, ILogger<AnimalService> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<AnimalViewModel>?> GetAllAsync(string? sortingField, string? sortingOrder, string? filteringString)
        {
            try
            {
                var data = await _appDbContext.Animals.Include(x => x.Facility).Include(x => x.Specie).ThenInclude(x => x.Type).ToListAsync();
                if (data == null)
                {
                    throw new NotFoundException(Message.MSG_NORECORDS);
                }

                return data.ConvertAll(f => f.ToViewModel()).FilterBy(filteringString).SortBy(sortingField, sortingOrder).ToList();
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
                var data = await _appDbContext.Animals.Include(x => x.Facility).Include(x => x.Specie).ThenInclude(x => x.Type).ToListAsync();

                if (data == null)
                {
                    throw new NotFoundException(Message.MSG_NORECORDS);
                }

                return data.Find(f => f.Id == id)?.ToViewModel();
            }
            catch (Exception ex)
            {
                _logger.LogError(Message.ERROR, ex.Message);
                return null;
            }
        }

        public async Task<AnimalViewModel?> AddAsync(AnimalDto dto)
        {
            try
            {
                var datas = await _appDbContext.Animals.ToListAsync();

                if (datas.Any(x => string.Equals(x.Name, dto.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    throw new BadRequestException(Message.MSG_NAMEINUSE);
                }

                var specie = await _appDbContext.Species.FirstOrDefaultAsync(t => t.Id == dto.SpecieId);
                var facility = await _appDbContext.Facilities.FirstOrDefaultAsync(t => t.Id == dto.FacilityId);

                if (specie == null || facility == null)
                {
                    throw new BadRequestException();
                }

                var data = dto.NewFromDto(specie, facility);

                await _appDbContext.Animals.AddAsync(data);
                await _appDbContext.SaveChangesAsync();
                _logger.LogInformation(Message.MSG_CREATED, data.Id);

                return data.ToViewModel();
            }
            catch (Exception ex)
            {
                _logger.LogError(Message.ERROR, ex.Message);
                return null;
            }
        }

        public async Task<AnimalViewModel?> UpdateAsync(Guid id, AnimalDto dto)
        {
            try
            {
                var datas = await _appDbContext.Animals.Include(x => x.Facility).Include(x => x.Specie).ToListAsync();

                if (datas == null)
                {
                    throw new NotFoundException(Message.MSG_NORECORDS);
                }

                if (datas.Any(x => string.Equals(x.Name, dto.Name, StringComparison.OrdinalIgnoreCase) && x.Id != id))
                {
                    throw new BadRequestException(Message.MSG_NAMEINUSE);
                }

                var data = datas.Find(f => f.Id == id);

                var specie = await _appDbContext.Species.FirstOrDefaultAsync(t => t.Id == dto.SpecieId);
                var facility = await _appDbContext.Facilities.FirstOrDefaultAsync(t => t.Id == dto.FacilityId);

                if (data == null || specie == null || facility == null)
                {
                    throw new BadRequestException();
                }

                data = data.UpdateFromDto(dto, specie, facility);

                _appDbContext.Entry(data).State = EntityState.Modified;

                await _appDbContext.SaveChangesAsync();
                _logger.LogInformation(Message.MSG_UPDATED, data.Id);

                return data.ToViewModel();
            }
            catch (Exception ex)
            {
                _logger.LogError(Message.ERROR, ex.Message);
                return null;
            }
        }

        public async Task<Guid?> DeleteAsync(Guid id)
        {
            try
            {
                var datas = await _appDbContext.Animals.ToListAsync();

                if (datas == null)
                {
                    throw new NotFoundException(Message.MSG_NORECORDS);
                }

                var data = datas.Find(f => f.Id == id);

                if (data == null)
                {
                    throw new NotFoundException(Message.MSG_NORECORDS);
                }

                _appDbContext.Animals.Remove(data);
                await _appDbContext.SaveChangesAsync();
                _logger.LogInformation(Message.MSG_DELETED, data.Id);

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