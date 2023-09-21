using Microsoft.EntityFrameworkCore;
using PerfectMatchBack.Models;
using PerfectMatchBack.Services.Contract;

namespace PerfectMatchBack.Services.Implementation
{
    public class PostService : IPostService
    {
        private PetFectMatchContext _context;
        public PostService(PetFectMatchContext context)
        {

            _context = context;

        }
        public async Task<Publication> addPublication(Publication model)
        {
            try
            {
                _context.Publications.Add(model);
                await _context.SaveChangesAsync();
                return model;

            } catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<bool> deletePublication(Publication model)
        {
            try
            {
                var ima = await _context.Publications.Include(id => id.Images).FirstOrDefaultAsync(id => id.IdPublication == model.IdPublication);
                _context.Images.RemoveRange(ima.Images);

                _context.Publications.Remove(model);
                await _context.SaveChangesAsync();
                return true;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<Publication> GetPublication(int id)
        {
            try
            {
                var publication = await _context.Publications.Include(navi => navi.IdOwnerNavigation).Include(navi => navi.IdCityNavigation).Include(navi => navi.IdAnimalTypeNavigation)
                    .Include(navi => navi.IdBreedNavigation).Include(navi => navi.Images)
                    .Where(model => model.IdPublication == id).FirstOrDefaultAsync()
                    ;
                return publication;

            } catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<List<Image>> listImage(int id)
        {
            try
            {
                var images = await _context.Images.Where(ide => ide.IdPublicationNavigation.IdPublication == id).ToListAsync();
                return images;
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Publication>> listPublication()
        {
            try
            {
                var list = await _context.Publications.Include(navi => navi.IdOwnerNavigation).Include(navi => navi.IdCityNavigation).Include(navi => navi.IdAnimalTypeNavigation).Include(navi => navi.IdBreedNavigation).
                    Include(id => id.IdGenderNavigation).Include(navi => navi.Images).
                    ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> updatePublication(Publication model)
        {
            try
            {
             
                
                _context.Publications.Update(model);

                await _context.SaveChangesAsync();
                return true;

            }catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<List<Publication>> userPublications(int idUser)
        {
            try
            {
                var list = await _context.Publications.Include(navi => navi.IdOwnerNavigation).Include(navi => navi.IdCityNavigation).Include(navi => navi.IdAnimalTypeNavigation).Include(navi => navi.IdBreedNavigation).
                    Include(id => id.IdGenderNavigation).Include(navi => navi.Images).Where(id => id.IdOwner == idUser).ToListAsync();
                return list;

            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
