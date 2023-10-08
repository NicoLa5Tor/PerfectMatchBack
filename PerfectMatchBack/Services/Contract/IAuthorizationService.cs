using PerfectMatchBack.Models.Custom;

namespace PerfectMatchBack.Services.Contract
{
    public interface IAuthorizationService
    {
        Task<AuthorizationResponse> ReturnToken(AuthorizationRequest aut);
        Task<AuthorizationResponse> ReturnRefreshToken(RefreshTokenRequest aut, int idUser);

    }
}
