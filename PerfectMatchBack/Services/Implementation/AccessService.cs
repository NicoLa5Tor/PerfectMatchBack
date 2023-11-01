using Microsoft.EntityFrameworkCore;
using PerfectMatchBack.Models;
using PerfectMatchBack.Services.Contract;

namespace PerfectMatchBack.Services.Implementation
{
    public class AccessService : IAccessService
    {
        private PetFectMatchContext _context;
        public AccessService(PetFectMatchContext context)
        {
            this._context = context;
        }

        public async Task<Access> CreateAccess(Access access)
        {
            try
            {
                _context.Accesses.Add(access);
                await _context.SaveChangesAsync();  
                return access;

            }catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<bool> DeleteAccess(Access access)
        {
            try
            {
                _context.Accesses.Remove(access);   
                await _context.SaveChangesAsync();
                return true;  

            }catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<Access> GetAccess(int id)
        {
            try
            {
                var access = await _context.Accesses.Where(model => model.IdAccess == id).FirstOrDefaultAsync();
                return access;

            }catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<List<Access>> ListAccess()
        {
            try
            {
                var list = await _context.Accesses.ToListAsync();
                return list;

            }catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<bool> UpdateAccess(Access access)
        {
            try
            {
               _context.Accesses.Update(access);
                await _context.SaveChangesAsync();
                return true;

            }catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
