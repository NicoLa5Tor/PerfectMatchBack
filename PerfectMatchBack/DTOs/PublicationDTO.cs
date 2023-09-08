namespace PerfectMatchBack.DTOs
{
    public class PublicationDTO
    {
        public int IdPublication { get; set; }

        public int? IdOwner { get; set; }
        public string? NameOwner { get; set; }  

        public string? AnimalName { get; set; }

        public int? IdCity { get; set; }
        public string? NameCity { get; set; }   


        public double? Weight { get; set; }

        public bool? Sex { get; set; }

        public int? Age { get; set; }

        public int IdAnimalType { get; set; }
        public string? NameType { get; set; }    

        public int IdBreed { get; set; }

        public string? NameBreed { get; set; }  

        public string? Description { get; set; }
    }
}
