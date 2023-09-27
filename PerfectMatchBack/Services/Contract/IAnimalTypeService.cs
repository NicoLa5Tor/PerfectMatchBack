using PerfectMatchBack.Models;

namespace PerfectMatchBack.Services.Contract
{
    public interface IAnimalTypeService
    {
        Task<List<AnimalType>> ListAnimalType();
    }
}
