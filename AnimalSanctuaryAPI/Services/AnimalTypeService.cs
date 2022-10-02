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
    public sealed class AnimalTypeService : IAnimalTypeService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<AnimalTypeService> _logger;

        public AnimalTypeService(AppDbContext appDbContext, ILogger<AnimalTypeService> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<AnimalTypeViewModel>?> GetAllAsync(string? sortingField, string? sortingOrder, string? filteringString)
        {
            try
            {
                var data = await _appDbContext.Types.ToListAsync();
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

        public async Task<AnimalTypeViewModel?> GetByIdAsync(Guid id)
        {
            try
            {
                var data = await _appDbContext.Types.ToListAsync();

                if (data == null)
                {
                    throw new NotFoundException(Message.MSG_NORECORDS);
                }

                return data.Find(d => d.Id == id)?.ToViewModel();
            }
            catch (Exception ex)
            {
                _logger.LogError(Message.ERROR, ex.Message);
                return null;
            }
        }

        public async Task<AnimalTypeViewModel?> AddAsync(AnimalTypeDto dto)
        {
            try
            {
                var datas = await _appDbContext.Types.ToListAsync();

                if (datas.Any(x => string.Equals(x.Name, dto.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    throw new BadRequestException(Message.MSG_NAMEINUSE);
                }

                var data = dto.NewFromDto();

                await _appDbContext.Types.AddAsync(data);
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

        public async Task<AnimalTypeViewModel?> UpdateAsync(Guid id, AnimalTypeDto dto)
        {
            try
            {
                var datas = await _appDbContext.Types.ToListAsync();

                if (datas == null)
                {
                    throw new NotFoundException(Message.MSG_NORECORDS);
                }

                if (datas.Any(x => string.Equals(x.Name, dto.Name, StringComparison.OrdinalIgnoreCase) && x.Id != id))
                {
                    throw new BadRequestException(Message.MSG_NAMEINUSE);
                }

                var data = datas.Find(f => f.Id == id);

                if (data == null)
                {
                    throw new BadRequestException();
                }

                data = data.UpdateFromDto(dto);

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
                var datas = await _appDbContext.Types.ToListAsync();

                if (datas == null)
                {
                    throw new NotFoundException(Message.MSG_NORECORDS);
                }

                var data = datas.Find(f => f.Id == id);

                if (data == null)
                {
                    throw new NotFoundException(Message.MSG_NORECORDS);
                }

                _appDbContext.Types.Remove(data);
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