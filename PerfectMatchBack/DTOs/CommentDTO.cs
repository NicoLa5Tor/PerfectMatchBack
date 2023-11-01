using PerfectMatchBack.Models;

namespace PerfectMatchBack.DTOs
{
    public class CommentDTO
    {
        public int IdComment { get; set; }
        public int? IdPublication { get; set; }
        public int? IdUser { get; set; }
        public int? IdCommentFk { get; set; }
        public string Comment1 { get; set; }
        public string NameOwnerComment { get; set; } 
        public string NameOwnerPublication { get; set; }
        public string NameUser { get; set; }

    }
}
