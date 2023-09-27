using Microsoft.EntityFrameworkCore;
using PerfectMatchBack.Models;
using PerfectMatchBack.Services.Contract;

namespace PerfectMatchBack.Services.Implementation
{
    public class RoleService : IRoleService
    {

        private PetFectMatchContext _context;   
        public RoleService(PetFectMatchContext context)
        {
            _context = context;
        }
        public async Task<List<Role>> listRole()
        {
            try
            {
                var list = await _context.Roles.ToListAsync();
                return list;
            }catch (Exception ex) {
                throw ex;
            }
        }
    }
}
