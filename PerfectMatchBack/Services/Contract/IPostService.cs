using PerfectMatchBack.DTOs;
using PerfectMatchBack.Models;

namespace PerfectMatchBack.Services.Contract
{
    public interface IPostService
    {
        Task<List<Publication>> ListPublication();
        Task<Publication> GetPublication(int id);
        Task<Publication> AddPublication(Publication model);
        Task<Publication> UpdatePublication(PublicationDTO model, int idPublication);
        Task<bool> DeletePublication(Publication model);
        Task<List<Image>> ListImage(int id);
        Task<List<Publication>> UserPublications(int idUser);
       
    }
}
