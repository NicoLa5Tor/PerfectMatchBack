using PerfectMatchBack.Models;

namespace PerfectMatchBack.Services.Contract
{
    public interface IRoleService
    {
        Task<List<Role>> ListRole();
    }
}
