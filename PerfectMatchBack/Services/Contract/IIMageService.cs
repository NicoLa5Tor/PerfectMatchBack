using PerfectMatchBack.DTOs;
using PerfectMatchBack.Models;

namespace PerfectMatchBack.Services.Contract
{
    public interface IImageService
    {
        Task<List<Image>> listImage();
        Task<Image> GetImage(int id);
        Task<Image> addImage(Image image);
        Task<bool> removeImage(Image image);
        Task<bool> removeRangeImage(List<ImageDTO> images);
        Task<bool> Updatemgae(Image image);
        Task<List<ImageDTO>> GetImageFromPublication(int idPublication);

    }
}
