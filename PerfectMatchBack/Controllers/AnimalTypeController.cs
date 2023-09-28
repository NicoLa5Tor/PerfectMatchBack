using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PerfectMatchBack.DTOs;
using PerfectMatchBack.Services.Contract;

namespace PerfectMatchBack.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AnimalTypeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAnimalTypeService _service;
        public AnimalTypeController(IMapper mapper,IAnimalTypeService service)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("List")]
        public async Task<IActionResult> ListAnimalType()
        {
            var list = await _service.listAnimalType();
                var listDTO = _mapper.Map<List<AnimalTypeDTO>>(list);
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
