using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PerfectMatchBack.DTOs;
using PerfectMatchBack.Models;
using PerfectMatchBack.Services.Contract;

namespace PerfectMatchBack.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private IImageService _service;
        private IMapper _mapper;
        public ImageController(IMapper mapper, IImageService service)
        {
            _service = service;
            _mapper = mapper;
        }
        [Authorize]
        [HttpGet("List")]
        public async Task<IActionResult> ListImage()
        {
            var list = await _service.listImage();
            var listDTO = _mapper.Map<List<ImageDTO>>(list);
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
        public async Task<IActionResult> AddImage(ImageDTO model)
        {
            var image = _mapper.Map<Image>(model);
            var imageAdd = await _service.addImage(image);
            if (imageAdd is not null)
            {
                return Ok(_mapper.Map<ImageDTO>(imageAdd));
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        //las imagenes solo se pueden actualizar para ser cambiadas, no se cambiaran el id de publicaci�n para no hacer mas tedioso el proceso
        [Authorize]
        [HttpPut("Update/{idImage}")]
        public async Task<IActionResult> UpdateImage(

            [FromRoute] int idImage,
            [FromBody] ImageDTO model

            )
        {
            var image = await _service.GetImage(idImage);
            if (image is null) return NotFound();
            var mapImage = _mapper.Map<Image>(model);
            image.DataImage = mapImage.DataImage;
            var result = await _service.Updatemgae(image);
            if (result)
            {
                return Ok(_mapper.Map<ImageDTO>(image));
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [Authorize]
        [HttpDelete("Delete/{idImage}")]
        public async Task<IActionResult> DeleteImage(
            int idImage
            )
        {
            var search = await _service.GetImage(idImage);
            if (search is null) return NotFound();
            var deleteImage = await _service.removeImage(search);
            if (deleteImage)
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
