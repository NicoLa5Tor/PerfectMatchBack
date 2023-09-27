using Microsoft.EntityFrameworkCore;
using PerfectMatchBack.Models;
using PerfectMatchBack.Services.Contract;

namespace PerfectMatchBack.Services.Implementation
{
    public class BreedService : IBreedService
    {
        private PetFectMatchContext _context;
        public BreedService (PetFectMatchContext context)
        {
            _context = context; 
        }

        public async Task<List<Breed>> listBreed()
        {
            try
            {
                var list = await _context.Breeds.Include(id => id.IdAnimalTypeNavigation).ToListAsync();
                return list;

            }catch (Exception ex) {
                throw ex;
            }
        }
    }
}
