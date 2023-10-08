using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PerfectMatchBack.Models;
using PerfectMatchBack.Services.Contract;
using System.Net;

namespace PerfectMatchBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private PetFectMatchContext _context;
        private readonly IReportService _reportService;
        private string ReportServerUrl = "";

        public ReportController(PetFectMatchContext context, IReportService reportService)
        {
            this._context = context;
            this._reportService = reportService;
        }
        [Authorize]

        [HttpGet("ReportType")]
        public async Task<List<string>> GetReportTypes()
        {
            return await _reportService.listReportType();
        }
        [Authorize]
        [HttpGet("ReportPath/{reportName}")]
        public async Task<string> GetReportPath(string reportName)
        {
            return await _reportService.GetReportPath(reportName);
        }
        [Authorize]
        [HttpGet("{reportName}")]
        public async Task<IActionResult> GetReport(string reportName)
        {
            using (var httpClient = new HttpClient(new HttpClientHandler
            {
                Credentials = new NetworkCredential("supervisor", "Sup123", "LAPTOP-COJ5AITI")
            }))
            {
                ReportServerUrl = await _reportService.GetServerUrl();
                var reportUrl = $"{ReportServerUrl}{reportName}&rs:Format=PDF";
                var response = await httpClient.GetAsync(reportUrl);

                if (response.IsSuccessStatusCode)
                {
                    var reportData = await response.Content.ReadAsByteArrayAsync();
                    return File(reportData, "application/pdf", $"{reportName}.pdf");
                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return Unauthorized();
                    }

                    return BadRequest();
                }
            }
        }
        [Authorize]
        [HttpGet("{reportName}/{param1Value}/{param2Value}")]
        public async Task<IActionResult> GetReportParams(string reportName, string param1Value, string param2Value)
        {
            using (var httpClient = new HttpClient(new HttpClientHandler
            {
                Credentials = new NetworkCredential("supervisor", "Sup123", "LAPTOP-COJ5AITI")
            }))
            {
                ReportServerUrl = await _reportService.GetServerUrl();
                // Construir la URL con los parámetros
                var reportUrl = $"{ReportServerUrl}{reportName}&rs:Format=PDF&first={param1Value}&last={param2Value}";

                var response = await httpClient.GetAsync(reportUrl);

                if (response.IsSuccessStatusCode)
                {
                    var reportData = await response.Content.ReadAsByteArrayAsync();
                    return File(reportData, "application/pdf", $"{reportName}.pdf");
                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return Unauthorized();
                    }

                    return BadRequest();
                }
            }
        }
    }
}