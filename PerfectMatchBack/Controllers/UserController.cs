using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PerfectMatchBack.DTOs;
using PerfectMatchBack.Models;
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

        public UserController(IMapper mapper, IUserService userService, IAccessService accessService)
        {
            _userService = userService;
            _mapper = mapper;
            _accessService = accessService;
        }

        /*[HttpPost]
        public async Task<IActionResult> Authenticate(UserAccessDTO userAccessDTO)
        {
            var response = new Response();
            var access = _mapper.Map<Access>(userAccessDTO.accessDto);
            var user = _mapper.Map<User>(userAccessDTO.userDto);
            var userResponse = await _userService.Auth(user, access);
            if (userResponse == null)
            {
                response.messageError = "User or pass incorrect";
                return BadRequest(response); 
            }
            response.success = 1;
            response.data=userResponse;
            return Ok(response);
        }*/
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
            {

            }
        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddUser(
    [FromBody] UserDTO model
    )
        {
            Encryption enc = new Encryption();
            Access access = new Access();
            //if (addAcces is null) return StatusCode(StatusCodes.Status500InternalServerError);

            var modelDTO = _mapper.Map<User>(model);
            //modelDTO.IdAccess = addAcces.IdAccess;
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
        [HttpPut("Update/{idUser}")]
        public async Task<IActionResult> UpdateUser(
    [FromRoute] UserDTO model,
    [FromRoute] int idUser
    )
        {
            var userTrue = await _userService.getUser(idUser);
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
        [HttpDelete("Delete/{idUser}")]
        public async Task<IActionResult> DeleteUser(
            [FromRoute] int idUser
            )
        {
            var userTrue = await _userService.getUser(idUser);
            if (userTrue is null) return NotFound();
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
    }
}
