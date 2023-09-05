namespace PerfectMatchBack.DTOs
{
    public class BreedDTO
    {
        public int IdBreed { get; set; }

        public string BreedName { get; set; } = null!;

        public int IdAnimalType { get; set; }

        public string? NameType { get; set; }
    }
}
