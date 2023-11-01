using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PerfectMatchBack.DTOs;
using PerfectMatchBack.Models;
using PerfectMatchBack.Services.Contract;
using PerfectMatchBack.Services.Implementation;

namespace PerfectMatchBack.Controllers
{
    
    [Route("[controller]")]
    [ApiController]
    public class PublicationController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IPostService _postService;
        public PublicationController(IMapper mapper, IPostService service)
        {
            _postService = service;
            _mapper = mapper;
        }
        [Authorize]
        [HttpGet("listImages/{id}")]
        public async Task<IActionResult> ListImagesById(
       [FromRoute] int id
       )
        {
            var list = await _postService.ListImage(id);
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
        [Authorize]
        [HttpGet("userList/{idUser}")]
        public async Task<IActionResult> ListPublicationsUser(
        [FromRoute] int idUser
        )
        {
            var list = await _postService.UserPublications(idUser);
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
        [Authorize]
        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> ListPublications()
        {
            var list = await _postService.ListPublication();
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
        [Authorize]
        [HttpPost("Add")]
        public async Task<IActionResult> AddPublication(PublicationDTO model)
        {
            var post = _mapper.Map<Publication>(model);
            var postAdd = await _postService.AddPublication(post);
            if (postAdd.IdPublication != 0)
            {
                return Ok(_mapper.Map<PublicationDTO>(postAdd));
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
        [Authorize]
        [HttpPut("Update/{idPublication}")]
        public async Task<IActionResult> UpdatePublication(int idPublication, PublicationDTO model)
        {


            var ouput = await _postService.UpdatePublication(model, idPublication);
            if (ouput != null)
            {
                return Ok(_mapper.Map<PublicationDTO>(ouput));
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [Authorize]
        [HttpDelete("Delete/{idPublication}")]
        public async Task<IActionResult> DeletePublication(
            int idPublication

            )
        {
            var postTrue = await _postService.GetPublication(idPublication);
            if (postTrue is null) return NotFound();
            var deletePost = await _postService.DeletePublication(postTrue);
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
