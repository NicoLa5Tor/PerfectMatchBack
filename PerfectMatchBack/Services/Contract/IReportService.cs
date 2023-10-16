using PerfectMatchBack.Models;

namespace PerfectMatchBack.Services.Contract
{
    public interface IReportService
    {
        Task<List<string>> ListReportType();
        Task<string> GetReportPath(string reportName);
        Task<string> GetServerUrl();
    }
}
