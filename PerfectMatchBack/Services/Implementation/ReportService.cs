using AspNetCore.Reporting;
using Microsoft.EntityFrameworkCore;
using PerfectMatchBack.Models;
using PerfectMatchBack.Services.Contract;
using PerfectMatchBack.DTOs;
using System.Reflection;
using System.Text;

namespace PerfectMatchBack.Services.Implementation
{
    public class ReportService : IReportService
    {
        private readonly PerfectMatchContext _dbContext;
        byte[] IReportService.GenerateReportAsync(string reportName, string reportType)
        {
            string fileDirPath = Assembly.GetExecutingAssembly().Location.Replace("PerfectMatchBack.dll",string.Empty);
            string rdlcDilePath = string.Format("{0}ReportFiles\\{1}.rdlc", fileDirPath,reportName);

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding.GetEncoding("utf-8");

            LocalReport report = new LocalReport(rdlcDilePath);

            //Prepare Data for report

            List<SaleDTO> movementList = (from movement in _dbContext.Movements
                                          join user in _dbContext.Users on movement.IdBuyer equals user.IdUser
                                          join publication in _dbContext.Publications on movement.IdPublication equals publication.IdPublication
                                          select new SaleDTO
                                          {
                                              Date = movement.Date.ToString(),
                                              Buyer = user.Name,
                                              Publication = publication.AnimalName,
                                              Amount = movement.Amount.ToString()
                                          }).ToList();

            report.AddDataSource("dsSales", movementList);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            var result = report.Execute(GetRenderType(reportType),1,parameters);


            return result.MainStream;
        }

        private RenderType GetRenderType(string reportType)
        {
            var renderType = RenderType.Pdf;

            switch (reportType.ToUpper())
            {
                default:
                case "PDF":
                    renderType = RenderType.Pdf;
                    break;
                case "XLS": 
                    renderType = RenderType.Excel; 
                    break;
                case "WORD":
                    renderType = RenderType.Word;
                    break;
            }
            return renderType;
        }
    }
}
