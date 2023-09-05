using PerfectMatchBack.Models;

namespace PerfectMatchBack.Services.Contract
{
    public interface IAccessService
    {
        Task<List<Access>> listAccess();
    }
}
