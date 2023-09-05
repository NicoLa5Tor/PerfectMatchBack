using Microsoft.EntityFrameworkCore;
using PerfectMatchBack.Models;
using PerfectMatchBack.Services.Contract;

namespace PerfectMatchBack.Services.Implementation
{
    public class AnimalTypeService : IAnimalTypeService
    {
        private PerfectMatchContext _context;
        public AnimalTypeService(PerfectMatchContext context)
        {
            this._context = context;
        }
        public async Task<List<AnimalType>> listAnimalType()
        {
            try {
                var list = await _context.AnimalTypes.ToListAsync();
                return list;

            }catch (Exception ex) {
                throw ex;
            }
        }
    }
}
