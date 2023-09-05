using PerfectMatchBack.Models;

namespace PerfectMatchBack.Services.Contract
{
    public interface ICityService
    {
        Task<List<City>> listCity();
    }
}
