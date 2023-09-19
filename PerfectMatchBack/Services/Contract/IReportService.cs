namespace PerfectMatchBack.Services.Contract
{
    public interface IReportService
    {
        byte[] GenerateReportAsync(string reportName, string reportType);
        
    }
}
