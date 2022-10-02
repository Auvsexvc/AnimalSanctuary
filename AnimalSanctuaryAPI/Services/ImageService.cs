using AnimalSanctuaryAPI.Data;
using AnimalSanctuaryAPI.Entities;
using AnimalSanctuaryAPI.Extensions;
using AnimalSanctuaryAPI.Helpers;
using AnimalSanctuaryAPI.Interfaces;
using AnimalSanctuaryAPI.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AnimalSanctuaryAPI.Services
{
    public sealed class ImageService : IImageService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<ImageService> _logger;

        public ImageService(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment, ILogger<ImageService> logger)
        {
            _appDbContext = appDbContext;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        public async Task<ImageViewModel?> UploadAsync(IFormFile file, Guid id)
        {
            try
            {
                var isAlreadyProfiled = await _appDbContext.Images.AnyAsync(x => x.ContextId == id);
                if (isAlreadyProfiled)
                {
                    return await ReplaceAsync(file, id);
                }

                var tempId = Guid.NewGuid();
                FileInfo fi = new(file.FileName);
                var newfilename = "Image_" + tempId + fi.Extension;
                var path = Path.Combine("", _webHostEnvironment.ContentRootPath + "\\Images\\" + newfilename);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                var data = new Image()
                {
                    Id = tempId,
                    Path = _webHostEnvironment.WebRootPath + "/Images/" + newfilename,
                    ContextId = id
                };
                _appDbContext.Images.Add(data);
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

        public async Task<ImageViewModel?> GetByIdAsync(Guid id)
        {
            var data = await _appDbContext.Images.FirstOrDefaultAsync(x => x.ContextId == id);

            return data?.ToViewModel();
        }

        public async Task<IEnumerable<ImageViewModel>> GetAllAsync()
        {
            var data = await _appDbContext.Images.ToListAsync();

            var datas = data.Select(d => d.ToViewModel());

            return datas;
        }

        private async Task<ImageViewModel?> ReplaceAsync(IFormFile file, Guid id)
        {
            try
            {
                var data = await _appDbContext.Images.FirstOrDefaultAsync(x => x.ContextId == id);

                if (data is null)
                {
                    return null;
                }

                var existingId = data.Id;
                FileInfo fi = new(file.FileName);
                var newfilename = "Image_" + existingId + fi.Extension;
                var path = Path.Combine("", _webHostEnvironment.ContentRootPath + "\\Images\\" + newfilename);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                data.Path = _webHostEnvironment.WebRootPath + "/Images/" + newfilename;

                _appDbContext.Update(data);

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
    }
}