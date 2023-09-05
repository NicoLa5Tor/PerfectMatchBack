namespace PerfectMatchBack.DTOs
{
    public class UserDTO
    {
        public int IdUser { get; set; }

        public int IdRole { get; set; }

        public string? NameRole { get; set; }   

        public string? Name { get; set; }

        public string? BirthDate { get; set; }

        public int? IdAccess { get; set; }

        public int? IdCity { get; set; }

        public string? NameCity { get; set; }   

        public string Email { get; set; } = null!;
    }
}
