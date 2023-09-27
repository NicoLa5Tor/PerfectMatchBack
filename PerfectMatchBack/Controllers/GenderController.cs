using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PerfectMatchBack.DTOs;
using PerfectMatchBack.Models;
using PerfectMatchBack.Services.Contract;

namespace PerfectMatchBack.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GenderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGenderService _service;
        public GenderController(IMapper mapper,IGenderService service)
        {
            _mapper = mapper;
            _service = service;
        }
        [HttpGet("List")]
        public async Task<IActionResult> ListGenders()
        {
            var list = await _service.ListGender();
            var listDTO = _mapper.Map<List<GenderDTO>>(list);
            if (listDTO.Count > 0)
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
