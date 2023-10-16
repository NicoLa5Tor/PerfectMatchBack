using PerfectMatchBack.Models;

namespace PerfectMatchBack.Services.Contract
{
    public interface IUserService
    {
        Task<List<User>> ListUser();
        Task<User> GetUser(int id);
        Task<List<User>> ListSellers();
        Task<bool> DeleteUser(User model);
        Task<bool> UpdateUser(User model);  
        Task<User> AddUser(User model);  

    }
}
