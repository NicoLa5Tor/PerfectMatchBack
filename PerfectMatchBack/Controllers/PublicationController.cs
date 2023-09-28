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

        [HttpGet("listImages/{id}")]
        public async Task<IActionResult> ListImagesById(
       [FromRoute] int id
       )
        {
            var list = await _service.listImage(id);
            var listDTO = _mapper.Map<List<ImageDTO>>(list);
            if (listDTO is not null)
            {
                return Ok(listDTO);
            }
            else
            {
                return NotFound();
            }

        }
        [HttpGet("userList/{idUser}")]
        public async Task<IActionResult> ListPublicationsUser(
        [FromRoute] int idUser
        )
        {
            var list = await _service.userPublications(idUser);
            var listDTO = _mapper.Map<List<PublicationDTO>>(list);
            if (listDTO is not null)
            {
                return Ok(listDTO);

            }
            else
            {
                return NotFound();
            }

        }
        [HttpGet("List")]
        public async Task<IActionResult> ListPublications()
        {
            var list = await _service.listPublication();
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
            var postAdd = await _service.addPublication(post);
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
           [FromBody] PublicationDTO model,
            IImageService _serviceImage
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
            if (publication.Images.Count > 0)
            {
                foreach (var im in publication.Images)
                {
                    var images = await _serviceImage.GetImage(im.IdImage);
                    if (images is not null)
                    {
                        images.DataImage = im.DataImage;
                        await _serviceImage.Updatemgae(images);
                    }

                }
            }
            var ouput = await _service.updatePublication(modelTrue);
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
            var deletePost = await _service.deletePublication(postTrue);
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
