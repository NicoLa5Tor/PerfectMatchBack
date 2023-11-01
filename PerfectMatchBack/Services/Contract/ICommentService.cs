using PerfectMatchBack.DTOs;

namespace PerfectMatchBack.Services.Contract
{
    public interface ICommentService
    {
        Task<List<CommentDTO>> GetCommmentsFromPublication(int idPublication);
        Task<CommentDTO> AddComment(CommentDTO commentDTO);
    }
}
