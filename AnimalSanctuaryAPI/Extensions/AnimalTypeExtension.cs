using AnimalSanctuaryAPI.Dtos;
using AnimalSanctuaryAPI.Entities;
using AnimalSanctuaryAPI.ViewModels;

namespace AnimalSanctuaryAPI.Extensions
{
    public static class AnimalTypeExtension
    {
        public static AnimalTypeViewModel ToViewModel(this AnimalType data)
        {
            return new AnimalTypeViewModel()
            {
                Id = data.Id,
                Name = data.Name,
                Description = data.Description
            };
        }

        public static AnimalType NewFromDto(this AnimalTypeDto dto)
        {
            return new AnimalType()
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description
            };
        }

        public static AnimalType UpdateFromDto(this AnimalType data, AnimalTypeDto dto)
        {
            data.Name = dto.Name;
            data.Description = dto.Description;

            return data;
        }
    }
}