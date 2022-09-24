namespace WebApp.Models.Base
{
    public interface IModelBase
    {
        Guid Id { get; set; }
        string Name { get; set; }
    }
}