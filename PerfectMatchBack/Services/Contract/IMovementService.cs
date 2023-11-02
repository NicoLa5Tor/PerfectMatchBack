using PerfectMatchBack.DTOs;
using PerfectMatchBack.Models;

namespace PerfectMatchBack.Services.Contract
{
    public interface IMovementService
    {
        Task<List<MovementDTO>> GetMovements();
        Task<MovementDTO> AddMovement(MovementDTO movement);
    }
}
