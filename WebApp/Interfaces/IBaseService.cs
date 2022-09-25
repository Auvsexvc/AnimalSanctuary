using WebApp.Models;

namespace WebApp.Interfaces
{
    public interface IBaseService
    {
        Task<HttpResponseMessage?> CreateAsync<T>(T dto, string accessToken);

        Task<HttpResponseMessage?> DeleteAsync<T>(Guid id, string accessToken);

        Task<HttpResponseMessage?> EditAsync<T>(Guid id, T dto, string accessToken);

        Task<IEnumerable<T>?> GetAllAsync<T>(string? sortingField, string? sortingOrder, string? filteringString);

        Task<IEnumerable<T>?> GetAllAsync<T>();

        Task<T?> GetByIdAsync<T>(Guid id);

        SortingDropdowns GetSortingDropdownsVM<T>(T obj);
    }
}