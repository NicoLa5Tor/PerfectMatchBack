using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PerfectMatchBack.DTOs;
using PerfectMatchBack.Services.Contract;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PerfectMatchBack.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRoleService _service;
        public RoleController(IMapper mapper, IRoleService service)
        {
            _mapper = mapper;
            _service = service;
        }
        [Authorize]

        [HttpGet("List")]
        public async Task<IActionResult> ListRoles()
        {
            var list = await _service.listRole();
            var listDTO = _mapper.Map<List<RoleDTO>>(list);
            if(listDTO.Count > 0)
            {
                return Ok(listDTO);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
