
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PerfectMatchBack.Models.Custom;
using PerfectMatchBack.Services.Contract;
using System.IdentityModel.Tokens.Jwt;

namespace PerfectMatchBack.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public readonly IAuthorizationService _authorizationService;
        public LoginController( IAuthorizationService authorizationService)
        {
            this._authorizationService = authorizationService;
        }
        [HttpPost]
        [Route("Authenticate")]
        public async Task<IActionResult> Authenticate(
        [FromBody] 
        AuthorizationRequest aut
            )
        {
            var resultAut = await _authorizationService.ReturnToken(aut);
            if (resultAut is null) return Unauthorized();
            return Ok(resultAut);

        }
        [HttpPost]
        [Route("ObtainRefreshToken")]
        public async Task<IActionResult> Authenticate(
            [FromBody]
            RefreshTokenRequest refreshToken
            )
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenExpire = tokenHandler.ReadJwtToken(refreshToken.TokenExpire);
            if (tokenExpire.ValidTo > DateTime.UtcNow)
                return BadRequest(new AuthorizationResponse { Result = false, Message = "Token no ha expirado" });
            string idUser = tokenExpire.Claims.First(x =>
            x.Type == JwtRegisteredClaimNames.NameId
            ).Value.ToString();
            var authorizationResponse = await _authorizationService.ReturnRefreshToken(refreshToken, int.Parse(idUser));
            if (authorizationResponse.Result)
                return Ok(authorizationResponse);
            else
                return BadRequest(authorizationResponse);
        }
    }
}
