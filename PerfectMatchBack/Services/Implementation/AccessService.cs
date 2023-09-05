using Microsoft.EntityFrameworkCore;
using PerfectMatchBack.Models;
using PerfectMatchBack.Services.Contract;

namespace PerfectMatchBack.Services.Implementation
{
    public class AccessService : IAccessService
    {
        private PerfectMatchContext _context;
        public AccessService(PerfectMatchContext context)
        {
            this._context = context;
        }

        public async Task<List<Access>> listAccess()
        {
            try
            {
                var list = await _context.Accesses.ToListAsync();
                return list;

            }catch (Exception ex) {
                throw ex;
            }
        }
    }
}
