
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PerfectMatchBack.Models;
using PerfectMatchBack.Models.Custom;
using PerfectMatchBack.Services.Contract;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PerfectMatchBack.Services.Implementation
{
    public class AuthorizationService : IAuthorizationService
      

    {
        private readonly PetFectMatchContext _context;
        private readonly IConfiguration _configuration;
        Encryption encrip = new Encryption();
        EncryptXOR xOr = new EncryptXOR(); 
        
        public AuthorizationService(PetFectMatchContext _context,IConfiguration _configuration )
        {
            this._context = _context;   
            this._configuration = _configuration;
        }
        private string GenerateToken(string idUser)
        {
            var key = _configuration.GetValue<string>("JwtSettings:key");
            var keyBites = Encoding.ASCII.GetBytes(key);
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, idUser));
            var credentialsToken = new SigningCredentials(
                new SymmetricSecurityKey(keyBites),
                SecurityAlgorithms.HmacSha256Signature
                );
            var tokenDetails = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = credentialsToken
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDetails);
            string tokenC = tokenHandler.WriteToken(tokenConfig);
            return tokenC;
        }
        private string GenerateRefreshToken()
        {
            var byteArray = new byte[64];
            var refreshToken = "";
            using (var mg = RandomNumberGenerator.Create())
            {
                mg.GetBytes(byteArray);
                refreshToken = Convert.ToBase64String(byteArray);
            }
            return refreshToken;    
        }
        private async Task<AuthorizationResponse> SaveHistorialRefreshToken(
            int idUser,
            string token,
            string refreshToken
            )
        {
            var historialRefreshToken = new HistorialRefreshToken
            {
                IdUser = idUser,
                Token = token,
                RefreshToken = refreshToken,
                DateCreate = DateTime.UtcNow,
                DateExpiration = DateTime.UtcNow.AddDays(7)

            };
            await _context.HistorialRefreshTokens.AddAsync(historialRefreshToken);
            await _context.SaveChangesAsync();
            return new AuthorizationResponse { Token = token, RefreshToken = refreshToken,Result = true, Message = "OK"};
        }
        public async Task<AuthorizationResponse> ReturnToken(AuthorizationRequest aut)
        {
            try
            {
               
                var user = await _context.Users.FirstOrDefaultAsync(em => em.Email == aut.email);
                if (user is null) return await Task.FromResult<AuthorizationResponse>(null);
                var access = await _context.Accesses.FirstOrDefaultAsync(id => id.IdAccess == user.IdAccess);
               if (access is null) return await Task.FromResult<AuthorizationResponse>(null);
                aut.password = xOr.DecryptXOR(aut.password);
                aut.password = encrip.Encrypt(aut.password);
                if (aut.password == access.Password)
                {
                    string token = GenerateToken(user.IdUser.ToString());
                    string refreshToken = GenerateRefreshToken();
                    //  return new AuthorizationResponse() { Token = Token, Result = true, Message = "OK" };
                    return await SaveHistorialRefreshToken(user.IdUser, token, refreshToken);
                }
                else
                {
                    return new AuthorizationResponse() { Token = null, Result = false, Message = "Error" };
                }

            }
            catch (Exception ex) {
                throw ex; 
            }
           
           
        }

        public async Task<AuthorizationResponse> ReturnRefreshToken(RefreshTokenRequest aut,  int idUser)
        {
            try
            {
                var refreshTokenEn = await _context.HistorialRefreshTokens.FirstOrDefaultAsync(x =>
                x.Token == aut.TokenExpire &&
                x.RefreshToken == aut.RefreshToken &&
                x.IdUser == idUser
                );
                if (refreshTokenEn is null) return new AuthorizationResponse { Result = false, Message = "No existe refreshToken" };
                var refreshTokenGenerate = GenerateRefreshToken();
                var tokenGenerate = GenerateToken(idUser.ToString());
                return await SaveHistorialRefreshToken(idUser, tokenGenerate, refreshTokenGenerate);
            } catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
