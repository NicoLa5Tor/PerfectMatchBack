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
    public class PublicationController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IPostService _service;
        public PublicationController(IMapper mapper, IPostService service)
        {
            _service = service;
            _mapper = mapper;
        }
        [HttpGet("List")]
        public async Task<IActionResult> ListPublications()
        {
            var list = await _service.ListPublication();
            var listDTO = _mapper.Map<List<PublicationDTO>>(list);
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
        public async Task<IActionResult> AddPublication(PublicationDTO model)
        {
            var post = _mapper.Map<Publication>(model);
            var postAdd = await _service.AddPublication(post);
            if (postAdd.IdPublication != 0)
            {
                return Ok(_mapper.Map<PublicationDTO>(postAdd));
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
        [HttpPut("Update/{idPublication}")] 
        public async Task<IActionResult> UpdatePublication(
           [FromRoute] int idPublication,
           [FromBody] PublicationDTO model  
            
            )
        {
            var modelTrue = await _service.GetPublication(idPublication);
            if (modelTrue is null) return NotFound();
            var publication = _mapper.Map<Publication>(model);
            modelTrue.Age = publication.Age;
            modelTrue.Description = publication.Description;
            modelTrue.Comments = publication.Comments;
            modelTrue.AnimalName = publication.AnimalName;
            modelTrue.IdBreed = publication.IdBreed;
            modelTrue.IdAnimalType = publication.IdAnimalType;
            modelTrue.IdGender = publication.IdGender;
            modelTrue.Weight = publication.Weight;
            var ouput = await _service.UpdatePublication(modelTrue);
            if (ouput)
            {
                return Ok(_mapper.Map<PublicationDTO>(modelTrue));
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete("Delete/{idPublication}")]
        public async Task<IActionResult> DeletePublication(
            int idPublication,
            IPostService _service,
            IImageService _imageService

            ) 
        {
            var postTrue = await _service.GetPublication(idPublication);
            if (postTrue is null) return NotFound();

            var deletePost = await _service.DeletePublication(postTrue);
            if (deletePost)
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
