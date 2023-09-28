using PerfectMatchBack.Models;

namespace PerfectMatchBack.Services.Contract
{
    public interface IReportService
    {
        Task<List<string>> listReportType();
        Task<string> GetReportPath(string reportName);
        Task<string> GetServerUrl();
    }
}
