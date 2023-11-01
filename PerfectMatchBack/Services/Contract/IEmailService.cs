using PerfectMatchBack.DTOs.RecoverPass;
using PerfectMatchBack.Models.Response;

namespace PerfectMatchBack.Services.Contract
{
    public interface IEmailService
    {
        Task<Response> SendEmail(EmailDTO response);
        Task<Response> UpdatePass(NewPass response);
        Task<Response> ValidateToken(string token);
    }
}
