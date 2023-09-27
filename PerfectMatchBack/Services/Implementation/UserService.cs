using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PerfectMatchBack.Models;
using PerfectMatchBack.Models.Common;
using PerfectMatchBack.Models.Response;
using PerfectMatchBack.Services.Contract;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PerfectMatchBack.Services.Implementation
{
    public class UserService : IUserService
    { 
        private readonly PerfectMatchContext _context;
        //private readonly AppSettings _appSettings;
        public UserService(PerfectMatchContext context)
        {
        //    _appSettings = IOptions<AppSettings>.Value;
            _context = context; 
        }

        public async Task<UserResponse> Auth(User user,Access access)
        {
            User userFind=null;
            UserResponse response=new UserResponse();
            var pass = await _context.Accesses.Where(x => x.Password == access.Password).FirstOrDefaultAsync();
            if (pass == null)
                return null; 
            userFind = await _context.Users.Where(x => x.Email == user.Email && pass.IdAccess == x.IdAccess).FirstOrDefaultAsync();
            if(userFind == null)
                return null;
            response.Email=user.Email;
            response.Token = GetToken(userFind);
            return response; 
        }
        private string GetToken(User user)
        {
            var tokenhandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("_appSettings.secret");
            //var key = Encoding.ASCII.GetBytes(_appSettings.secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.IdUser.ToString()),
                        new Claim(ClaimTypes.Email, user.Email)
                    }),
                Expires = DateTime.UtcNow.AddMinutes(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenhandler.CreateToken(tokenDescriptor);
            return tokenhandler.WriteToken(token);
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
                _context.Users.Remove(model);
                await _context.SaveChangesAsync();
                return true;    


            }catch(Exception) {
                return false;
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

            var users = _context.Users.Include(navi=>navi.Publications).Include(nave => nave.IdCityNavigation).Include(navi => navi.IdRoleNavigation).ToList();
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
    }
}
