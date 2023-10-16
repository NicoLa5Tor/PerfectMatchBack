using PerfectMatchBack.DTOs.RecoverPass;
using PerfectMatchBack.Services.Contract;
using MailKit.Security;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MimeKit;
using static Org.BouncyCastle.Math.EC.ECCurve;
using PerfectMatchBack.Models;
using Microsoft.EntityFrameworkCore;
using PerfectMatchBack.Models.Response;
using AutoMapper;
using PerfectMatchBack.DTOs;

namespace PerfectMatchBack.Services.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly PetFectMatchContext _context;
        private readonly IUserService _userService;
        private readonly IAccessService _accessService;
        private readonly IMapper _mapper;
        public EmailService(IConfiguration config, PetFectMatchContext context, IUserService userService, IMapper mapper, IAccessService accessService)
        {
            _config = config;
            _context = context;
            _userService = userService;
            _mapper = mapper;
            _accessService = accessService;
        }
        public async Task<Response> ValidateToken(string token)
        {
            var response = new Response();
            try
            {
                var oRecover = await _context.RecoverPass.Where(x => x.Token == Encryption.GetSha256(token)).FirstOrDefaultAsync();
                if (oRecover == null)
                {
                    response.Message = "Token expired";
                    return response;
                }
                response.State = 1;
                response.Message = "Valid token";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Error: " + ex.Message;
                return response;
            }
        }
        public async Task<Response> SendEmail(EmailDTO Request)
        {
            var response = new Response();
            var token = Guid.NewGuid().ToString();
            try
            {
                //Save in database
                Request.Subject = "Recuperar contraseña";
                var user = await _context.Users.Where(x => x.Email == Request.Email).FirstOrDefaultAsync();
                if (user == null)
                {
                    response.Message = "The email is not found in the database";
                    return response;
                }
                var newToken = new RecoverPass { IdUser = user.IdUser, Token = Encryption.GetSha256(token) };
                await _context.AddAsync(newToken);
                await _context.SaveChangesAsync();


                Request.Content = "<h4>Mail to recover pass</h4><br> <a href='"
                    + _config.GetSection("Email:Domain").Value + "newpassword?token=" + token
                    + "'>Click to recover pass</a>";

                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_config.GetSection("Email:UserName").Value));
                email.To.Add(MailboxAddress.Parse(Request.Email));
                email.Subject = Request.Subject;
                email.Body = new TextPart(TextFormat.Html) { Text = Request.Content };

                //Configuration Server

                //Connection with the server
                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_config.GetSection("Email:Host").Value,
                    Convert.ToInt32(_config.GetSection("Email:Port").Value),
                    SecureSocketOptions.StartTls
                    );
                await smtp.AuthenticateAsync(_config.GetSection("Email:UserName").Value, _config.GetSection("Email:Password").Value);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
                response.State = 1;
                response.Message = "The email was send successfully";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Error:" + ex.Message;
                return response;
            }


        }

        public async Task<Response> UpdatePass(NewPass request)
        {
            var response = new Response();
            try
            {
                var oRecover = await _context.RecoverPass.Where(x => Encryption.GetSha256(request.Token) == x.Token).FirstOrDefaultAsync();
                if (oRecover == null)
                {
                    response.Message = "Token expired";
                    return response;
                }
                if (request.Pass != request.Pass2)
                {
                    response.Message = "Passwords do not match";
                    return response;
                }
                var oUser = _mapper.Map<User>(await _userService.GetUser(oRecover.IdUser));
                if (oUser == null)
                {
                    response.Message = "The user is not found";
                    return response;
                }
                var access = await _context.Accesses.Where(x => x.IdAccess == oUser.IdAccess).FirstOrDefaultAsync();
                if (access == null)
                {
                    response.Message = "The user doesn't have a password assigned";
                    return response;
                }
                access.Password = Encryption.Encrypt(request.Pass);
                await _accessService.UpdateAccess(access);
                _context.Remove(oRecover);
                await _context.SaveChangesAsync();
                response.State = 1;
                response.Message = "The user is updated";
                response.Data = _mapper.Map<UserDTO>(oUser);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Error:" + ex.Message;
                return response;
            }
        }
    }
}
