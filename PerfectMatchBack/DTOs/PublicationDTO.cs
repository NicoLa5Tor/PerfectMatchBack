using PerfectMatchBack.Models;

namespace PerfectMatchBack.DTOs
{
    public class PublicationDTO
    {
        public int IdPublication { get; set; }

        public int? IdOwner { get; set; }
        public string? NameOwner { get; set; }  

        public string? AnimalName { get; set; }

        public int? IdCity { get; set; }
        public string? CityName { get; set; }


        public int? IdGender { get; set; }
        public string? GenderName { get; set; }
        public double? Weight { get; set; }

        public bool? Sex { get; set; }

        public int? Age { get; set; }

        public int IdAnimalType { get; set; }
        public string? TypeName { get; set; }    

        public int IdBreed { get; set; }

        public string? BreedName { get; set; }  

        public string? Description { get; set; }
        public virtual ICollection<ImageDTO> Images { get; set; } = new List<ImageDTO>();
    }
}
