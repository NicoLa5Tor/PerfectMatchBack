using Microsoft.EntityFrameworkCore;
using PerfectMatchBack.Models;
using PerfectMatchBack.Services.Contract;

namespace PerfectMatchBack.Services.Implementation
{
    public class PostService : IPostService
    {
        private PerfectMatchContext _context;
        public PostService(PerfectMatchContext context)
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

            }catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<bool> deletePublication(Publication model)
        {
            try
            {
                _context.Publications.Remove(model);
                await _context.SaveChangesAsync();
                return true;

            }catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<Publication> GetPublication(int id)
        {
            try
            {
                var publication = await _context.Publications.Include(navi => navi.IdOwnerNavigation).Include(navi => navi.IdCityNavigation).Include(navi => navi.IdAnimalTypeNavigation).Include(navi => navi.IdBreedNavigation).
                    Where(model => model.IdPublication == id).FirstOrDefaultAsync();
                return publication;

            }catch (Exception ex) {
                throw ex;
            }
        }
        public async Task<List<Publication>> listPublication()
        {
            try
            {
                var list = await _context.Publications.Include(navi => navi.IdOwnerNavigation).Include(navi => navi.IdCityNavigation).Include(navi => navi.IdAnimalTypeNavigation).Include(navi => navi.IdBreedNavigation).
                    ToListAsync();
                return list;
            }
            catch(Exception ex)
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
    }
}
