using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PerfectMatchBack.DTOs;
using PerfectMatchBack.Models;
using PerfectMatchBack.Services.Contract;

namespace PerfectMatchBack.Services.Implementation
{
    public class CommentService : ICommentService
    {
        private readonly PetFectMatchContext _context;
        private readonly IMapper _mapper;
        public CommentService(PetFectMatchContext context,IMapper mapper) 
        { 
            _context = context;
            _mapper = mapper;
        }
        public async Task<CommentDTO> AddComment(CommentDTO commentDTO)
        {
            try
            {
                var comment = _mapper.Map<Comment>(commentDTO);
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return (_mapper.Map<CommentDTO>(comment));
            }catch(Exception)
            {
                return null;
            }
        }
        public async Task<List<CommentDTO>> GetCommmentsFromPublication(int idPublication)
        {
            return _mapper.Map<List<CommentDTO>> (await _context.Comments.Where(x=>x.IdPublication==idPublication)
                .Include(x=>x.IdPublicationNavigation.IdOwnerNavigation).Include(x=>x.IdUserNavigation)
                .Include(x=>x.IdCommentFkNavigation.IdUserNavigation).ToListAsync());
        }
    }
}
