using Microsoft.EntityFrameworkCore;
using PerfectMatchBack.Models;
using PerfectMatchBack.Services.Contract;

namespace PerfectMatchBack.Services.Implementation
{
    public class UserService : IUserService
    { 
        private PerfectMatchContext _context;
        public UserService(PerfectMatchContext context)
        {

            _context = context; 

        }
        public async Task<User> addUser(User model)
        {
            try
            {
                _context.Users.Add(model);
                await _context.SaveChangesAsync();  
                return model;

            }catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<bool> deleteUser(User model)
        {
            try
            {
                var pass = await _context.Accesses.FirstOrDefaultAsync(id => id.IdAccess == model.IdAccess);
                _context.Users.Remove(model);
                _context.Accesses.Remove(pass);
                await _context.SaveChangesAsync();
                return true;    
            }catch(Exception ex) {
                throw ex;
            }
        }

        public async Task<User> getUser(int id)

        {
            try {
                var user = await _context.Users.Include(navi => navi.IdAccessNavigation).Include(nave => nave.IdCityNavigation).Include(navi => navi.IdRoleNavigation).
         Where(model => model.IdUser == id).FirstOrDefaultAsync();

                return user;
            }
            catch(Exception ex) {
                throw ex;
            }
         
        }

        public  async Task<List<User>> listUser()
        {
            try
            {
                var list = await _context.Users.Include(navi => navi.IdAccessNavigation).Include(nave => nave.IdCityNavigation).Include(navi => navi.IdRoleNavigation).
                ToListAsync();
                return list;

            }
            catch(Exception ex) {

                throw ex; 
            }
          
        }

        public async Task<bool> updateUser(User model)
        {
            try
            {
                _context.Users.Update(model);
                await _context.SaveChangesAsync();
                return true;

            }catch(Exception ex)
            {

                throw ex;   
            }
        }
    }
}
