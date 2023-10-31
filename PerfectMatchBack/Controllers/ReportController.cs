using AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PerfectMatchBack.DTOs;
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
        [HttpGet("ServerReport/{reportName}/{userId}")]
        public async Task<IActionResult> GetReport(string reportName, int userId)
        {
            using (var httpClient = new HttpClient(new HttpClientHandler
            {
                Credentials = new NetworkCredential("supervisor", "Sup123", "LAPTOP-COJ5AITI")
            }))
            {
                ReportServerUrl = await _reportService.GetServerUrl();
                // Construir la URL con los parámetros
                var reportUrl = $"{ReportServerUrl}{reportName}&rs:Format=PDF&UserID={userId}";

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
        //[Authorize]
        [HttpGet("TableList/{reportName}/{idUser}")]
        public async Task<IActionResult> GetAllSalesPurchase(string reportName, int idUser)
        {
            var movementList = new List<PurchaseSaleDTO>();
            if (reportName == "SalesReport")
            {
                movementList = await _reportService.GetAllSales(idUser);
            }
            if (reportName == "PurchaseReport")
            {
                movementList = await _reportService.GetAllPurchase(idUser);
            }

            if (movementList.Count > 0)
            {
                return Ok(movementList);
            }
            else
            {
                return NotFound("No movements");
            }
        }

        [HttpGet("GetRDLC/{idUser}/{reportName}/{startDate}/{endDate}")]
        public ActionResult GetRDLC(int idUser, string reportName, string startDate, string endDate)
        {
            var reportFileByteString = _reportService.GenerateDateReportAsync(idUser, reportName, startDate, endDate);
            var fileName = reportName;

            return File(reportFileByteString, "application/pdf", fileName);
        }

        [HttpGet("GetGraphInfo/{startDate}/{endDate}")]
        public async Task<ActionResult> GetGraphInfo(string startDate, string endDate)
        {
            var newUserList = await _reportService.GetGraphNewUser(startDate, endDate);

            if (newUserList.Count > 0)
            {
                return Ok(newUserList);
            }
            else
            {
                return NotFound("No users");
            }
        }

        [HttpGet("GetTableInfo/{startDate}/{endDate}")]
        public async Task<ActionResult> GetTableInfo(string startDate, string endDate)
        {
            var newUserList = await _reportService.GetTableNewUser(startDate, endDate);

            if (newUserList.Count > 0)
            {
                return Ok(newUserList);
            }
            else
            {
                return NotFound("No users");
            }
        }

        [HttpGet("GetTableMov/{startDate}/{endDate}")]
        public async Task<ActionResult> GetTableMov(string startDate, string endDate)
        {
            var movementList = await _reportService.GetMovementsBetweenDates(startDate, endDate);

            if (movementList.Count > 0)
            {
                return Ok(movementList);
            }
            else
            {
                return NotFound("No movements");
            }
        }

    }
}