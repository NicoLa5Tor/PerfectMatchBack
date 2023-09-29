using PerfectMatchBack.Models;

namespace PerfectMatchBack.Services.Contract
{
    public interface IPostService
    {
        Task<List<Publication>> listPublication();
        Task<Publication> GetPublication(int id);
        Task<Publication> addPublication(Publication model);
        Task<bool> updatePublication(Publication model);
        Task<bool> deletePublication(Publication model);
        Task<List<Image>> listImage(int id);
        Task<List<Publication>> userPublications(int idUser);
       
    }
}
