using AspNetCore.Reporting;
using Microsoft.EntityFrameworkCore;
using PerfectMatchBack.DTOs;
using PerfectMatchBack.Models;
using PerfectMatchBack.Services.Contract;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace PerfectMatchBack.Services.Implementation
{
    public class ReportService : IReportService
    {
        private PetFectMatchContext _context;
        private int idUser;

        public ReportService()
        {

        }
        public ReportService(PetFectMatchContext context)
        {
            this._context = context;
        }

        public async Task<List<PurchaseSaleDTO>> GetAllPurchase(int idBuyer)
        {
            try
            {
                var purchases = await _context.Movements
                            .Where(m => m.IdBuyer == idBuyer)
                            .Join(_context.Users, m => m.IdSeller, u => u.IdUser, (m, u) => new { m, u })
                            .Join(_context.Publications, mu => mu.m.IdPublication, p => p.IdPublication, (mu, p) => new { mu, p })
                            .Select(result => new PurchaseSaleDTO
                            {
                                Date = result.mu.m.Date.ToString(),
                                Name = result.mu.u.Name,
                                Publication = result.p.AnimalName,
                                Amount = result.mu.m.Amount.HasValue ? result.mu.m.Amount.Value.ToString("F") : string.Empty
                            })
                            .ToListAsync();
                return purchases;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<PurchaseSaleDTO>> GetAllSales(int idSeller)
        {
            try
            {
                var sales = await _context.Movements
                            .Where(m => m.IdSeller == idSeller)
                            .Join(_context.Users, m => m.IdBuyer, u => u.IdUser, (m, u) => new { m, u })
                            .Join(_context.Publications, mu => mu.m.IdPublication, p => p.IdPublication, (mu, p) => new { mu, p })
                            .Select(result => new PurchaseSaleDTO
                            {
                                Date = result.mu.m.Date.ToString(),
                                Name = result.mu.u.Name,
                                Publication = result.p.AnimalName,
                                Amount = result.mu.m.Amount.HasValue ? result.mu.m.Amount.Value.ToString("F") : string.Empty
                            })
                            .ToListAsync();
                return sales;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> GetReportPath(string reportName)
        {
            try
            {
                var path = await _context.ReportPaths
                                .Where(rp => rp.ReportName == reportName)
                                .Select(rp => rp.Path)
                                .FirstOrDefaultAsync();
                return path;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> GetServerUrl()
        {
            try
            {
                var path = await _context.ReportPaths
                                .Where(rp => rp.IdReport == 1)
                                .Select(rp => rp.Path)
                                .FirstOrDefaultAsync();
                return path;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<string>> listReportType()
        {
            try
            {
                var list = await _context.ReportPaths
                                .Where(rp => rp.IdReport > 1)
                                .Select(rp => rp.ReportName)
                                .ToListAsync();
                return list;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Byte[] GenerateDateReportAsync(int idUser, string reportName, string startDate, string endDate)
        {

            string fileDirPatch = Assembly.GetExecutingAssembly().Location.Replace("PerfectMatchBack.dll", string.Empty);
            string rdlcFilePath = string.Format("{0}ReportFiles\\{1}.rdlc", fileDirPatch, reportName);

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding.GetEncoding("utf-8");

            LocalReport report = new LocalReport(rdlcFilePath);

            Dictionary<string, string> parameters = new Dictionary<string, string>();


            if (reportName == "SalesReport")
            {
                List<SaleDTO> salesList = GetSalesDataBetweenDates(idUser).Result;
                report.AddDataSource("dsSales", salesList);
            }
            else if (reportName == "PurchaseReport")
            {
                List<PurchaseDTO> purchasesList = GetPurchasesDataBetweenDates(idUser).Result;
                report.AddDataSource("dsPurchase", purchasesList);
            }
            else if (reportName == "NewUserReport")
            {
                parameters.Add("startDate", startDate);
                parameters.Add("endDate", endDate);
                List<TableNewUserDTO> tableList = GetTableNewUser(startDate, endDate).Result;
                report.AddDataSource("dsTableUser", tableList);

            }
            else if (reportName == "AllMovementReport")
            {
                parameters.Add("startDate", startDate);
                parameters.Add("endDate", endDate);
                List<AllMovementDTO> tableList = GetMovementsBetweenDates(startDate, endDate).Result;
                report.AddDataSource("dsMovement", tableList);

            }


            var renderType = RenderType.Pdf;
            var result = report.Execute(renderType, 1, parameters, "");

            return result.MainStream;
        }

        public async Task<List<AllMovementDTO>> GetMovementsBetweenDates(string startDateStr, string endDateStr)
        {
            DateTime startDate;
            DateTime endDate;

            if (DateTime.TryParse(startDateStr, out startDate) && DateTime.TryParse(endDateStr, out endDate))
            {
                return await _context.Movements
                    .Include(m => m.IdBuyerNavigation)
                    .Include(m => m.IdSellerNavigation)
                    .Include(m => m.IdPublicationNavigation)
                    .Where(m => m.Date >= startDate && m.Date <= endDate)
                    .OrderByDescending(m => m.Date) 
                    .Select(m => new AllMovementDTO
                    {
                        Date = m.Date.ToString(),
                        Buyer = m.IdBuyerNavigation != null ? m.IdBuyerNavigation.Name : "N/A",
                        Seller = m.IdSellerNavigation != null ? m.IdSellerNavigation.Name : "N/A",
                        Publication = m.IdPublicationNavigation != null ? m.IdPublicationNavigation.AnimalName : "N/A",
                        Amount = m.Amount.HasValue ? m.Amount.Value.ToString("F") : string.Empty
                    })
                    .ToListAsync();
            }

            return null;
        }



        public async Task<List<SaleDTO>> GetSalesDataBetweenDates(int idUser)
        {
            return await _context.Movements
                            .Where(m => m.IdSeller == idUser)
                            .Join(_context.Users, m => m.IdBuyer, u => u.IdUser, (m, u) => new { m, u })
                            .Join(_context.Publications, mu => mu.m.IdPublication, p => p.IdPublication, (mu, p) => new { mu, p })
                            .Select(result => new SaleDTO
                            {
                                Date = result.mu.m.Date.ToString(),
                                Buyer = result.mu.u.Name,
                                Publication = result.p.AnimalName,
                                Amount = result.mu.m.Amount.HasValue ? result.mu.m.Amount.Value.ToString("F") : string.Empty
                            })
                            .ToListAsync(); ;
        }

        public async Task<List<PurchaseDTO>> GetPurchasesDataBetweenDates(int idUser)
        {
            return await _context.Movements
                            .Where(m => m.IdBuyer == idUser)
                            .Join(_context.Users, m => m.IdSeller, u => u.IdUser, (m, u) => new { m, u })
                            .Join(_context.Publications, mu => mu.m.IdPublication, p => p.IdPublication, (mu, p) => new { mu, p })
                            .Select(result => new PurchaseDTO
                            {
                                Date = result.mu.m.Date.ToString(),
                                Seller = result.mu.u.Name,
                                Publication = result.p.AnimalName,
                                Amount = result.mu.m.Amount.HasValue ? result.mu.m.Amount.Value.ToString("F") : string.Empty
                            })
                            .ToListAsync();
        }

        public async Task<List<TableNewUserDTO>> GetTableNewUser(string startDateStr, string endDateStr)
        {
            DateTime startDate;
            DateTime endDate;

            if (DateTime.TryParse(startDateStr, out startDate) && DateTime.TryParse(endDateStr, out endDate))
            {
                var usersData = await _context.Users
                    .Where(u => u.AccountDate >= startDate && u.AccountDate <= endDate)
                    .Select(u => new
                    {
                        u.AccountDate,
                        Year = u.AccountDate.Value.Year
                    })
                    .ToListAsync();

                var result = usersData
                    .Select(u => new
                    {
                        WeekNumber = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear((DateTime)u.AccountDate, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday),
                        u.Year
                    })
                    .GroupBy(u => new
                    {
                        WeekNumber = u.WeekNumber,
                        u.Year
                    })
                    .Select(g => new
                    {
                        WeekNumber = g.Key.WeekNumber,
                        YearNumber = g.Key.Year,
                        UserQuantity = g.Count()
                    })
                    .Where(a => a.UserQuantity > 0)
                    .OrderBy(a => a.YearNumber)
                    .ThenBy(a => a.WeekNumber)
                    .ToList();

                var tableData = result.Select(r =>
                {
                    int year = r.YearNumber;
                    int week = r.WeekNumber;
                    DateTime firstDayOfWeek = FirstDateOfWeekISO8601(year, week);
                    DateTime lastDayOfWeek = firstDayOfWeek.AddDays(6);

                    return new TableNewUserDTO
                    {
                        Year = year.ToString(),
                        Week = week.ToString(),
                        StartDate = firstDayOfWeek.ToString("yyyy-MM-dd"),
                        EndDate = lastDayOfWeek.ToString("yyyy-MM-dd"),
                        UserQuantity = r.UserQuantity
                    };
                }).ToList();

                return tableData;
            }

            return null;
        }

        // Función para calcular el primer día de la semana ISO 8601
        DateTime FirstDateOfWeekISO8601(int year, int week)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(jan1, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            if (firstWeek <= 1)
            {
                week -= 1;
            }

            return firstThursday.AddDays(week * 7);
        }


        public async Task<List<DTOs.GraphNewUserDTO>> GetGraphNewUser(string startDateStr, string endDateStr)
        {
            DateTime startDate;
            DateTime endDate;

            if (DateTime.TryParse(startDateStr, out startDate) && DateTime.TryParse(endDateStr, out endDate))
            {
                var usersData = await _context.Users
                    .Where(u => u.AccountDate >= startDate && u.AccountDate <= endDate)
                    .Select(u => new
                    {
                        u.AccountDate,
                        Year = u.AccountDate.Value.Year
                    })
                    .ToListAsync();

                var result = usersData
                    .Select(u => new
                    {
                        YearNumber = u.Year,
                        WeekNumber = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear((DateTime)u.AccountDate, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday)
                    })
                    .GroupBy(u => new
                    {
                        YearNumber = u.YearNumber,
                        WeekNumber = u.WeekNumber
                    })
                    .Select(g => new
                    {
                        YearNumber = g.Key.YearNumber,
                        WeekNumber = g.Key.WeekNumber,
                        UserQuantity = g.Count()
                    })
                    .OrderBy(a => a.YearNumber)
                    .ThenBy(a => a.WeekNumber)
                    .ToList();

                return result.Select(r => new DTOs.GraphNewUserDTO
                {
                    YearNumber = r.YearNumber,
                    WeekNumber = r.WeekNumber,
                    UserQuantity = r.UserQuantity,
                }).ToList();
            }

            return null;
        }

    }
        
}
