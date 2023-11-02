using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PerfectMatchBack.DTOs;
using PerfectMatchBack.Services.Contract;

namespace PerfectMatchBack.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MovementController : ControllerBase
    {
        private readonly IMovementService _movementService;
        public MovementController(IMovementService movementService)
        {
            _movementService = movementService;
        }
        [HttpGet("GetMovements")]
        public async Task<IActionResult> GetMovements()
        {
            return Ok(await _movementService.GetMovements());
        }
        [HttpPost("AddMovement")]
        public async Task<IActionResult> AddMovements([FromBody]MovementDTO movement )
        {
            return Ok(await _movementService.AddMovement(movement));
        }
    }
}
