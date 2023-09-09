using Microsoft.EntityFrameworkCore;
using PerfectMatchBack.Models;
using PerfectMatchBack.Services.Contract;

namespace PerfectMatchBack.Services.Implementation
{
    public class GenderService : IGenderService
    {
        private PerfectMatchContext _context;
        public GenderService(PerfectMatchContext context)
        {
            _context = context;
        }

        public async Task<List<Gender>> listGender()
        {
            try
            {
                var list = await _context.Genders.ToListAsync();
                return list;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
