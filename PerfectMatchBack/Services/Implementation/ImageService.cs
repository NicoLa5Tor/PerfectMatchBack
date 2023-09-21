using Microsoft.EntityFrameworkCore;
using PerfectMatchBack.Models;
using PerfectMatchBack.Services.Contract;

namespace PerfectMatchBack.Services.Implementation
{
    public class ImageService : IIMageService
    {
        private PetFectMatchContext _context;
        public ImageService(PetFectMatchContext context)
        {

            _context = context;

        }
        public async Task<Image> addImage(Image image)
        {
            try
            {
                _context.Images.Add(image);
                await _context.SaveChangesAsync();
                return image;   

            }catch (Exception ex) {
                throw ex;
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
                throw ex;
            }
        }

        public async Task<List<Image>> listImage()
        {
            try
            {
                var list = await _context.Images.Include(naviga => naviga.IdPublicationNavigation).ToListAsync();
                return list;    
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       

        public async Task<bool> removeImage(Image image)
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

        public async Task<bool> Updatemgae(Image image)
        {
            try
            {
                if (image is not null) {
                    _context.Images.Update(image);
                }
                 await _context.SaveChangesAsync();

                return true;    

            } catch (Exception ex) {
                throw ex;
            }

        }
    }
}
