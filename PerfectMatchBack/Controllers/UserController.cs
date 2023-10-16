using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PerfectMatchBack.DTOs;
using PerfectMatchBack.DTOs.RecoverPass;
using PerfectMatchBack.Models;
using PerfectMatchBack.Models.Response;
using PerfectMatchBack.Services.Contract;
using PerfectMatchBack.Services.Implementation;
using System.Globalization;

namespace PerfectMatchBack.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private IMapper _mapper;
        private IEmailService _emailService;
        private readonly IAccessService _accessService;
        private readonly INotificationService _notificationService;


        public UserController(INotificationService notificationservice, IMapper mapper, IUserService userService, IAccessService accessService, IEmailService emailService)
        {
            _userService = userService;
            _mapper = mapper;
            _accessService = accessService;
            _emailService = emailService;
            _notificationService = notificationservice;
        }
        [HttpPost("Notification")]
        public async Task<IActionResult> AddNotification(NotificationDTO notification)
        {
            try
            {
                await _notificationService.AddNotification(notification);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Notification/{id}")]
        public async Task<IActionResult> GetNotifications(int id)
        {
            try
            {
                return Ok(await _notificationService.GetNotifications(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("Notification/{id}")]
        public async Task<IActionResult> RemoveNotifications(int id)
        {
            var response = new Response();
            try
            {
                await _notificationService.RemoveNotification(id);
                response.State = 1;
                response.Message = "Notificacion eliminada";
                return Ok();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpPost("GenerateToken")]
        public async Task<IActionResult> RecoverPassword(EmailDTO Request)
        {
            try
            {
                var response = await _emailService.SendEmail(Request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPost("NewPassword")]
        public async Task<IActionResult> RecoverPassword([FromBody] NewPass Request)
        {
            try
            {
                var response = await _emailService.UpdatePass(Request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet("ValidationToken")]
        public async Task<IActionResult> ValidationToken([FromQuery] string token)
        {
            try
            {
                var response = await _emailService.ValidateToken(token);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpGet("List")]
        public async Task<IActionResult> ListUsers()
        {
            var list = await _userService.ListUser();
            var listDTO = _mapper.Map<List<UserDTO>>(list);
            if (listDTO.Count > 0)
            {
                return Ok(listDTO);
            }
            else
            {
                return NotFound();
            }
        }
        [Authorize]
        [HttpGet("Seller")]
        public async Task<IActionResult> ListSellers()
        {
            var list = await _userService.ListSellers();
            var listDTO = _mapper.Map<List<UserDTO>>(list);
            if (listDTO.Count > 0)
            {
                return Ok(listDTO);
            }
            else
            {
                return NotFound();
            }
        }
        [Authorize]
        [HttpPost("Add")]
        public async Task<IActionResult> AddUser([FromBody] UserDTO model)
        {
            
            Access access = new Access();
            access.Password = Encryption.Encrypt(model.password);

            var addAcces = await _accessService.CreateAccess(access);
            if (addAcces is null) return StatusCode(StatusCodes.Status500InternalServerError);
            model.IdAccess = addAcces.IdAccess;
            var modelDTO = _mapper.Map<User>(model);
            modelDTO.IdAccess = addAcces.IdAccess;
            var addUser = await _userService.AddUser(modelDTO);

            if (addUser is not null)
            {
                return Ok(_mapper.Map<UserDTO>(addUser));
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
        [Authorize]
        [HttpPut("Update/{idUser}")]
        public async Task<IActionResult> UpdateUser([FromBody] UserDTO model,[FromRoute] int idUser)
        {
            var userTrue = await _userService.GetUser(idUser);
            if (userTrue is null) return StatusCode(StatusCodes.Status500InternalServerError);
            userTrue.Name = model.Name;
            userTrue.Email = model.Email;
            userTrue.IdCity = model.IdCity;
            userTrue.BirthDate = DateTime.ParseExact(model.BirthDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            userTrue.IdRole = model.IdRole;
            var userUpdate = await _userService.UpdateUser(userTrue);
            if (userUpdate)
            {
                return Ok(_mapper.Map<UserDTO>(userTrue));
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
        [Authorize]
        [HttpDelete("Delete/{idUser}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int idUser)
        {
            var userTrue = await _userService.GetUser(idUser);
            if (userTrue is null) return NotFound();
            var access = await _accessService.GetAccess(userTrue.IdAccess);
            var deleteUser = await _userService.DeleteUser(userTrue);
            if (deleteUser)
            {

                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPut("UpdatePass/{idPass}")]
        public async Task<IActionResult> UpdatePassword(AccessDTO access, int idPass)
        {
            var acces = await _accessService.GetAccess(idPass);
            if (acces is null) return NotFound();
            acces.Password = Encryption.Encrypt(access.Password);
            var updateAcces = await _accessService.UpdateAccess(acces);
            if (updateAcces) {
                return Ok(_mapper.Map<AccessDTO>(acces));
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
    }
}
