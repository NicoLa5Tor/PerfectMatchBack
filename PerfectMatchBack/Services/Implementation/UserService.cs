using Microsoft.EntityFrameworkCore;
using PerfectMatchBack.Models;
using PerfectMatchBack.Services.Contract;

namespace PerfectMatchBack.Services.Implementation
{
    public class UserService : IUserService
    { 
        private PetFectMatchContext _context;
        public UserService(PetFectMatchContext context)
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
        public async Task<List<User>> listSellers()
        {

            var users = _context.Users.Include(navi => navi.Publications).Include(nave => nave.IdCityNavigation).Include(navi => navi.IdRoleNavigation).ToList();
            var sellers = new List<User>();
            try
            {
                users.ForEach(x => { if (x.Publications.Count > 0) sellers.Add(x); });
                await _context.SaveChangesAsync();
                return sellers;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> EmailExist(string email)
        {
            try
            {
                var emailTrue = await _context.Users.FirstOrDefaultAsync(e => e.Email == email);
                if (emailTrue is null) return true;
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
