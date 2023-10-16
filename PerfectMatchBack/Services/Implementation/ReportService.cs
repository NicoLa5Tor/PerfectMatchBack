using Microsoft.EntityFrameworkCore;
using PerfectMatchBack.Models;
using PerfectMatchBack.Services.Contract;

namespace PerfectMatchBack.Services.Implementation
{
    public class ReportService : IReportService
    {
        private PetFectMatchContext _context;
        public ReportService(PetFectMatchContext context)
        {
            this._context = context;
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

        public async Task<List<string>> ListReportType()
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
    }
}
