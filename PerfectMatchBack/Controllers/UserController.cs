using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PerfectMatchBack.DTOs;
using PerfectMatchBack.Models;
using PerfectMatchBack.Models.Response;
using PerfectMatchBack.Services.Contract;
using System.Globalization;

namespace PerfectMatchBack.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private IMapper _mapper;
        private readonly IAccessService _accessService;
        private readonly INotificationService _notificationService;

        public UserController(INotificationService notificationservice,  IMapper mapper, IUserService userService, IAccessService accessService)
        {
            _userService = userService;
            _mapper = mapper;
            _accessService = accessService;
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
        [HttpGet("Notifications/{id}")]
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

        [HttpGet("List")]
        public async Task<IActionResult> ListUsers()
        {
            var list = await _userService.listUser();
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
            var list = await _userService.listSellers();
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
       
        [HttpPost("Add")]
        public async Task<IActionResult> AddUser(
    [FromBody] UserDTO model
    )
        {
            
            Access access = new Access();
            access.Password = Encryption.Encrypt(model.password);

            var addAcces = await _accessService.CreateAccess(access);
            if (addAcces is null) return StatusCode(StatusCodes.Status500InternalServerError);
            model.IdAccess = addAcces.IdAccess;
            var modelDTO = _mapper.Map<User>(model);
            modelDTO.IdAccess = addAcces.IdAccess;
            var addUser = await _userService.addUser(modelDTO);

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
        [HttpGet("GetUser/{idUser}")]
        public async Task<IActionResult> GetUser(int idUser)
        {
            var user = await _userService.GetUser(idUser);
            if(user is null) return NotFound();
            var maper = _mapper.Map<UserDTO>(user);
            return Ok(maper.IdRole);
        }
        [Authorize]
        [HttpGet("GetTotalUser/{idUser}")]
        public async Task<IActionResult> GetTotalUser(int idUser)
        {
            var user = await _userService.GetUser(idUser);
            if (user is null) return NotFound();
            var maper = _mapper.Map<UserDTO>(user);
            return Ok(maper);
        }
        [Authorize]
        [HttpPut("Update/{idUser}")]
        public async Task<IActionResult> UpdateUser(
    [FromRoute] UserDTO model,
    [FromRoute] int idUser
    )
        {
            var userTrue = await _userService.GetUser(idUser);
            if (userTrue is null) return StatusCode(StatusCodes.Status500InternalServerError);
            userTrue.Name = model.Name;
            userTrue.Email = model.Email;
            userTrue.IdCity = model.IdCity;
            userTrue.BirthDate = DateTime.ParseExact(model.BirthDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            userTrue.IdRole = model.IdRole;
            var userUpdate = await _userService.updateUser(userTrue);
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
        public async Task<IActionResult> DeleteUser(
            [FromRoute] int idUser
            )
        {
            var userTrue = await _userService.GetUser(idUser);
            if (userTrue is null) return NotFound();
            var access = await _accessService.GetAccess((int)userTrue.IdAccess);
            var deleteUser = await _userService.deleteUser(userTrue);
            if (deleteUser)
            {

                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
        [Authorize]
        [HttpPut("UpdatePass/{idPass}")]
        public async Task<IActionResult> UpdatePassword(
             AccessDTO access,
             int idPass

            )
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
        [HttpGet("EmailExist/{email}")]
        public async Task<IActionResult> EmailExist(string email)
        {
            if (email is not null) { 
            var result = await _userService.EmailExist(email);
            return Ok(result);
            }
            else
            {
                return Ok("todo bien, todo correcto");
            }
        }
    }
}
