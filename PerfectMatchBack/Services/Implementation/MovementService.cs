using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PerfectMatchBack.DTOs;
using PerfectMatchBack.Models;
using PerfectMatchBack.Services.Contract;

namespace PerfectMatchBack.Services.Implementation
{
    public class MovementService:IMovementService
    {
        private PetFectMatchContext _context;
        private readonly IMapper _mapper;
        public MovementService(PetFectMatchContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MovementDTO> AddMovement(MovementDTO movement)
        {
            var movement1 = _mapper.Map<Movement>(movement);
            await _context.AddAsync(movement1);
            await _context.SaveChangesAsync();
            movement = _mapper.Map<MovementDTO>(movement1);
            return movement;

        }

        public async Task<List<MovementDTO>> GetMovements()
        {
            var list = (await _context.Movements.ToListAsync());
            list.Reverse();
            return _mapper.Map<List<MovementDTO>>(list);
        }
    }
}
