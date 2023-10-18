using PerfectMatchBack.Models;

namespace PerfectMatchBack.Services.Contract
{
    public interface IUserService
    {
        Task<List<User>> listUser();
        Task<User> getUser(int id);
        Task<List<User>> listSellers();
        Task<bool> deleteUser(User model);
        Task<bool> updateUser(User model);  
        Task<User> addUser(User model);
        Task<bool> EmailExist(string email);

    }
}
