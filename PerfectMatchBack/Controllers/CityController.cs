using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PerfectMatchBack.DTOs;
using PerfectMatchBack.Services.Contract;

namespace PerfectMatchBack.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly ICityService _service;

        public CityController(IMapper mapper, ICityService service)
        {
            _mapper = mapper; 
            _service = service;
        }

        [HttpGet("List")]
        public async Task<IActionResult> ListCity()
        {
            var list = await _service.listCity();
                var listDTO = _mapper.Map<List<CityDTO>>(list);
            if (listDTO.Count > 0) {
            return Ok(listDTO);
            }
            else
            {
            return NotFound();
            }
        }
    }
}
