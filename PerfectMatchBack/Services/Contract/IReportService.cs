using PerfectMatchBack.DTOs;
using PerfectMatchBack.Models;

namespace PerfectMatchBack.Services.Contract
{
    public interface IReportService
    {
        Task<List<string>> listReportType();
        Task<string> GetReportPath(string reportName);
        Task<string> GetServerUrl();
        Task<List<PurchaseSaleDTO>> GetAllSales(int idSeller);
        Task<List<PurchaseSaleDTO>> GetAllPurchase(int idBuyer);
        Task<Byte[]> GenerateDateReportAsync(int idUser, string reportName, string startDate, string endDate);

        Task<List<TableNewUserDTO>> GetTableNewUser(string startDateStr, string endDateStr);
        Task<List<DTOs.GraphNewUserDTO>> GetGraphNewUser(string startDateStr, string endDateStr);

        Task<List<AllMovementDTO>> GetMovementsBetweenDates(string startDateStr, string endDateStr);
    }
}
