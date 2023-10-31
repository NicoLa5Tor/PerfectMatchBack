using PerfectMatchBack.Models;

namespace PerfectMatchBack.Services.Contract
{
    public interface IAccessService
    {
        Task<List<Access>> listAccess();
        Task<Access> getAccess(int id);
        Task<bool> updateAccess(Access access);
        Task<bool> deleteAccess(Access access);
        Task<Access> createAccess(Access access);
    }
}
