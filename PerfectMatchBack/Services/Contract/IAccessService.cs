using PerfectMatchBack.Models;

namespace PerfectMatchBack.Services.Contract
{
    public interface IAccessService
    {
        Task<List<Access>> ListAccess();
        Task<Access> GetAccess(int id);
        Task<bool> UpdateAccess(Access access);
        Task<bool> DeleteAccess(Access access);

        Task<Access> CreateAccess(Access access);
    }
}
