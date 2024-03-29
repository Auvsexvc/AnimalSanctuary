﻿using AnimalSanctuaryAPI.Data;
using AnimalSanctuaryAPI.Dtos;
using AnimalSanctuaryAPI.Exceptions;
using AnimalSanctuaryAPI.Extensions;
using AnimalSanctuaryAPI.Helpers;
using AnimalSanctuaryAPI.Interfaces;
using AnimalSanctuaryAPI.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AnimalSanctuaryAPI.Services
{
    public sealed class AnimalSpecieService : IAnimalSpecieService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<AnimalSpecieService> _logger;

        public AnimalSpecieService(AppDbContext appDbContext, ILogger<AnimalSpecieService> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<AnimalSpecieViewModel>?> GetAllAsync(string? sortingField, string? sortingOrder, string? filteringString)
        {
            try
            {
                var data = await _appDbContext.Species.Include(x => x.Type).ToListAsync();
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

        public async Task<AnimalSpecieViewModel?> GetByIdAsync(Guid id)
        {
            try
            {
                var data = await _appDbContext.Species.Include(x => x.Type).ToListAsync();

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

        public async Task<AnimalSpecieViewModel?> AddAsync(AnimalSpecieDto dto)
        {
            try
            {
                var datas = await _appDbContext.Species.ToListAsync();

                if (datas.Any(x => string.Equals(x.Name, dto.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    throw new BadRequestException(Message.MSG_NAMEINUSE);
                }

                var type = await _appDbContext.Types.FirstOrDefaultAsync(t => t.Id == dto.TypeId);

                if (type == null)
                {
                    throw new BadRequestException();
                }

                var data = dto.NewFromDto(type);

                await _appDbContext.Species.AddAsync(data);
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

        public async Task<AnimalSpecieViewModel?> UpdateAsync(Guid id, AnimalSpecieDto dto)
        {
            try
            {
                var datas = await _appDbContext.Species.Include(x => x.Type).ToListAsync();

                if (datas == null)
                {
                    throw new NotFoundException(Message.MSG_NORECORDS);
                }

                if (datas.Any(x => string.Equals(x.Name, dto.Name, StringComparison.OrdinalIgnoreCase) && x.Id != id))
                {
                    throw new BadRequestException(Message.MSG_NAMEINUSE);
                }

                var data = datas.Find(f => f.Id == id);
                var type = await _appDbContext.Types.FirstOrDefaultAsync(t => t.Id == dto.TypeId);

                if (data == null || type == null)
                {
                    throw new BadRequestException();
                }

                data = data.UpdateFromDto(dto, type);

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
                var datas = await _appDbContext.Species.ToListAsync();

                if (datas == null)
                {
                    throw new NotFoundException(Message.MSG_NORECORDS);
                }

                var data = datas.Find(f => f.Id == id);

                if (data == null)
                {
                    throw new NotFoundException(Message.MSG_NORECORDS);
                }

                _appDbContext.Species.Remove(data);
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