using PerfectMatchBack.Models;

namespace PerfectMatchBack.Services.Contract
{
    public interface IImageService
    {
        Task<List<Image>> ListImage();
        Task<Image> GetImage(int id);
        Task<Image> AddImage(Image image);
        Task<bool> RemoveImage(Image image);
        Task<bool> UpdateImage(Image image);
    }
}
