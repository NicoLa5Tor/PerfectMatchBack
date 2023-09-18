namespace PerfectMatchBack.DTOs
{
    public class CityDTO
    {
        public int IdCity { get; set; }

        public string CityName { get; set; } = null!;
        public int? IdDepartment { get; set; }
        public string? DepartmentName { get; set;}
    }
}
