using WebApp.Models;

namespace WebApp.Interfaces
{
    public interface IAnimalSanctuaryService
    {
        int Create(Animal animal);

        void Delete(int? id);

        IEnumerable<Animal> GetAll();

        Animal GetById(int? id);

        bool IsEnoughSpace();

        void Update(Animal animal);
        int GetSpaceLeft();
    }
}