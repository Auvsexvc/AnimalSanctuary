using WebApp.Data;
using WebApp.Dtos;
using WebApp.Helpers;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Services
{
    public class ImageService
    {
        private readonly IConfiguration _configuration;
        private readonly IBaseService _baseService;
        private readonly ILogger<ImageService> _logger;

        public ImageService(IConfiguration configuration, IBaseService baseService, ILogger<ImageService> logger)
        {
            _configuration = configuration;
            _baseService = baseService;
            _logger = logger;
        }

        public async Task<ImageModel?> GetByIdAsync(Guid id)
        {
            try
            {
                var obj = await _baseService.GetByIdAsync<Image>(id);
                if (obj == null)
                {
                    return null;
                }

                ImageModel? data = new()
                {
                    Id = obj.Id,
                    ContextId = obj.ContextId,
                    Path = _configuration.GetConnectionString("DefaultConnection") + obj.Path,
                };

                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(Message.ERROR, ex.Message);

                return null;
            }
        }
    }
}