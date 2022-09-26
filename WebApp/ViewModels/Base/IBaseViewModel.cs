namespace WebApp.ViewModels.Base
{
    public interface IBaseViewModel
    {
        Guid Id { get; set; }
        string Name { get; set; }
    }
}