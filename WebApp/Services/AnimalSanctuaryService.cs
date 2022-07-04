using AutoMapper;
using WebApp.Data;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Services
{
    public class AnimalSanctuaryService : IAnimalSanctuaryService
    {
        private readonly AnimalSanctuaryDbContext _context;
        private readonly IMapper _mapper;
        private const int _maxSanctuarySpace = 15;

        public AnimalSanctuaryService(AnimalSanctuaryDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Create(Animal animal)
        {
            var animalDb = _mapper.Map<Animal>(animal);
            _context.Add(animalDb);
            _context.SaveChanges();
            return animalDb.Id;
        }

        public void Delete(int? id)
        {
            var animalDb = _context.Animals.Find(id);
            if (animalDb != null)
            {
                _context.Animals.Remove(animalDb);
                _context.SaveChanges();
            }
        }



        public IEnumerable<Animal> GetAll()
        {
            var animals = _context.Animals.ToList();
            var animalsDb = _mapper.Map<List<Animal>>(animals);
            return animalsDb;
        }

        public Animal GetById(int? id)
        {
            var animal = _context.Animals.Find(id);
            var animalDb = _mapper.Map<Animal>(animal);
            return animalDb;
        }

        public bool IsEnoughSpace()
        {
            return GetAll().Count() <= _maxSanctuarySpace;
        }

        public int GetSpaceLeft()
        {
            return _maxSanctuarySpace - GetAll().Count();
        }

        public void Update(Animal animal)
        {
            var animalDb = _context.Animals.Find(animal.Id);
            if (animalDb != null)
            {
                animalDb.Id = animal.Id;
                animalDb.Name = animal.Name;
                animalDb.Type = animal.Type;
                animalDb.Sex = animal.Sex;
                animalDb.HealthState = animal.HealthState;
                _context.SaveChanges();
            }
        }
    }
}