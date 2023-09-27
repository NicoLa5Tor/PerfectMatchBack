using PerfectMatchBack.Models;

namespace PerfectMatchBack.Services.Contract
{
    public interface IPostService
    {
        Task<List<Publication>> ListPublication();
        Task<Publication> GetPublication(int id);
        Task<Publication> AddPublication(Publication model);
        Task<bool> UpdatePublication(Publication model);
        Task<bool> DeletePublication(Publication model);
    }
}
