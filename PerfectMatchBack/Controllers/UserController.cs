using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PerfectMatchBack.DTOs;
using PerfectMatchBack.Models;
using PerfectMatchBack.Models.Response;
using PerfectMatchBack.Services.Contract;

namespace PerfectMatchBack.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private IMapper _mapper;
        public UserController(IMapper mapper,IUserService userService)
        {
            _userService = userService;
            _mapper = mapper;
        }
        
        [HttpPost]
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
        [HttpGet("Seller")]
        public async Task<IActionResult>  ListSellers()
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
    }
}
