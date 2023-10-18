using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PerfectMatchBack.DTOs;
using PerfectMatchBack.Services.Contract;

namespace PerfectMatchBack.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BreedController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IBreedService _service;
        public BreedController(IMapper mapper,IBreedService service)
        {
            _service = service;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("List")]
        public async Task<IActionResult> ListBreed() 
        { 
           var list = await _service.listBreed();
                var listDTO = _mapper.Map<List<BreedDTO>>(list);
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
