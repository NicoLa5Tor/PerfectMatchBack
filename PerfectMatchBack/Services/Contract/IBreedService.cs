using PerfectMatchBack.Models;

namespace PerfectMatchBack.Services.Contract
{
    public interface IBreedService
    {
        Task<List<Breed>> listBreed();
    }
}
