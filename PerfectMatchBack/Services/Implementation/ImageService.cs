using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PerfectMatchBack.DTOs;
using PerfectMatchBack.Models;
using PerfectMatchBack.Services.Contract;

namespace PerfectMatchBack.Services.Implementation
{
    public class ImageService : IImageService
    {
        private readonly PetFectMatchContext _context;
        private readonly IMapper _map;
        public ImageService(PetFectMatchContext context, IMapper mapper)
        {
            _context = context;
            _map = mapper;
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

        public async Task<bool> removeRangeImage(List<ImageDTO> images)
        {
            try
            {
                var images1 = _map.Map<List<Image>>(images);
                _context.RemoveRange(images1);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ImageDTO>> GetImageFromPublication(int idPublication)
        {
            return _map.Map<List<ImageDTO>>(await (from x in _context.Images where x.IdPublication == idPublication select x).ToListAsync());

        }

    }
}
