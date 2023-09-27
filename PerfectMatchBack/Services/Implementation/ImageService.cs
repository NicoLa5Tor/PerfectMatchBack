using Microsoft.EntityFrameworkCore;
using PerfectMatchBack.Models;
using PerfectMatchBack.Services.Contract;

namespace PerfectMatchBack.Services.Implementation
{
    public class ImageService : IImageService
    {
        private PerfectMatchContext _context;
        public ImageService(PerfectMatchContext context)
        {
            _context = context;
        }
        public async Task<Image> AddImage(Image image)
        {
            try
            {
                _context.Images.Add(image);
                await _context.SaveChangesAsync();
                return image;   

            }catch (Exception) {
                return null;
            }
        }

        public async Task<Image> GetImage(int id)
        {
            try
            {
                var image = await _context.Images.Include(navi => navi.IdPublicationNavigation).Where
                    (model => model.IdImage == id).FirstOrDefaultAsync();
                return image;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Image>> ListImage()
        {
            try
            {
                var list = await _context.Images.Include(naviga => naviga.IdPublicationNavigation).ToListAsync();
                return list;    
            }
            catch (Exception)
            {
                return new();
            }
        }

      

        public async Task<bool> RemoveImage(Image image)
        {
            try
            {
                _context.Images.Remove(image);
                await _context.SaveChangesAsync();
                return true;

            }catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<bool> UpdateImage(Image image)
        {
            try
            {
                _context.Images.Update(image);
                await _context.SaveChangesAsync();  
                return true;    

            } catch (Exception) {
                return false;
            }

        }
    }
}
