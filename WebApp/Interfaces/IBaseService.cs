using WebApp.Models;

namespace WebApp.Interfaces
{
    public interface IBaseService
    {
        Task<HttpResponseMessage?> CreateAsync<T>(T dto);

        Task<HttpResponseMessage?> DeleteAsync<T>(Guid id);

        Task<HttpResponseMessage?> EditAsync<T>(Guid id, T dto);

        Task<IEnumerable<T>?> GetAllAsync<T>(string? sortingField, string? sortingOrder, string? filteringString);

        Task<T?> GetByIdAsync<T>(Guid id);

        SortingDropdowns GetSortingDropdownsVM<T>(T obj);
    }
}