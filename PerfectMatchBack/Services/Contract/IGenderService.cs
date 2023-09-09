using PerfectMatchBack.Models;

namespace PerfectMatchBack.Services.Contract
{
    public interface IGenderService
    {
        Task<List<Gender>> listGender();
    }
}
