using WebClientApp.Dtos;
using WebClientApp.ViewModels;

namespace WebClientApp.Interfaces
{
    public interface IBaseService
    {
        string ApiUri { get; }

        Task<HttpResponseMessage?> CreateAsync<T>(T dto, string accessToken);

        Task<HttpResponseMessage?> DeleteAsync<T>(Guid id, string accessToken);

        Task<HttpResponseMessage?> EditAsync<T>(Guid id, T dto, string accessToken);

        Task<IEnumerable<T>?> GetAllAsync<T>(string? sortingField, string? sortingOrder, string? filteringString);

        Task<IEnumerable<T>?> GetAllAsync<T>();

        Task<T?> GetByIdAsync<T>(Guid id);

        SortingDropdownsViewModel GetSortingDropdownsVM<T>(T obj);

        Task<HttpResponseMessage?> PostImageAsync(ImageDto dto, string accessToken);
    }
}