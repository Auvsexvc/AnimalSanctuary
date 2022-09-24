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
    public class FacilityService : IFacilityService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<FacilityService> _logger;

        public FacilityService(AppDbContext appDbContext, ILogger<FacilityService> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<FacilityViewModel>?> GetViewModels(string? sortingField, string? sortingOrder, string? filteringString)
        {
            try
            {
                var data = await _appDbContext.Facilities.Include(x => x.Animals).ToListAsync();
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

        public async Task<FacilityViewModel?> GetViewModel(Guid id)
        {
            try
            {
                var data = await _appDbContext.Facilities.Include(x => x.Animals).ToListAsync();

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

        public async Task<FacilityViewModel?> Add(FacilityDto dto)
        {
            try
            {
                var datas = await _appDbContext.Facilities.ToListAsync();

                if (datas.Any(x => string.Equals(x.Name, dto.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    throw new BadRequestException(Message.MSG_NAMEINUSE);
                }

                var data = dto.NewFromDto();

                await _appDbContext.Facilities.AddAsync(data);
                await _appDbContext.SaveChangesAsync();
                _logger.LogInformation(string.Format(Message.MSG_CREATED, data.Id), data.Id);

                return data.ToViewModel();
            }
            catch (Exception ex)
            {
                _logger.LogError(Message.ERROR, ex.Message);
                return null;
            }
        }

        public async Task<FacilityViewModel?> Update(Guid id, FacilityDto dto)
        {
            try
            {
                var datas = await _appDbContext.Facilities.Include(x => x.Animals).ToListAsync();

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
                    throw new NotFoundException(Message.MSG_NORECORDS);
                }

                data = data.UpdateFromDto(dto);

                _appDbContext.Entry(data).State = EntityState.Modified;

                await _appDbContext.SaveChangesAsync();
                _logger.LogInformation(string.Format(Message.MSG_UPDATED, data.Id), data.Id);

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
                var datas = await _appDbContext.Facilities.ToListAsync();

                if (datas == null)
                {
                    throw new NotFoundException(Message.MSG_NORECORDS);
                }

                var data = datas.Find(f => f.Id == id);

                if (data == null)
                {
                    throw new NotFoundException(Message.MSG_NORECORDS);
                }

                _appDbContext.Facilities.Remove(data);
                await _appDbContext.SaveChangesAsync();
                _logger.LogInformation(string.Format(Message.MSG_DELETED, data.Id), data.Id);

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