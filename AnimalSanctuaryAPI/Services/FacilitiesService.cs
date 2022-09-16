using AnimalSanctuaryAPI.Data;
using AnimalSanctuaryAPI.Dtos;
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

        public async Task<FacilityViewModel?> GetViewModel(Guid id)
        {
            var data = await _appDbContext.Facilities.Include(x => x.Animals).ToListAsync();

            if (data == null)
            {
                return null;
            }

            try
            {
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
            var data = dto.NewFromDto();

            try
            {
                await _appDbContext.Facilities.AddAsync(data);
                await _appDbContext.SaveChangesAsync();

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
                var datas = await _appDbContext.Facilities.ToListAsync();

                if (datas == null)
                {
                    return null;
                }

                var data = datas.Find(f => f.Id == id);

                if (data == null)
                {
                    return null;
                }

                _appDbContext.Facilities.Remove(data);
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