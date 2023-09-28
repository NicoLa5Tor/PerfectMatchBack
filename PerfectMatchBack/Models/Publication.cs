using System;
using System.Collections.Generic;

namespace PerfectMatchBack.Models;

public partial class Publication
{
    public int IdPublication { get; set; }

    public int? IdOwner { get; set; }

    public string? AnimalName { get; set; }

    public int? IdCity { get; set; }

    public double? Weight { get; set; }

    public int IdGender { get; set; }

    public int? Age { get; set; }

    public int IdAnimalType { get; set; }

    public int IdBreed { get; set; }

    public string? Description { get; set; }

    public long? Price { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual AnimalType IdAnimalTypeNavigation { get; set; } = null!;

    public virtual Breed IdBreedNavigation { get; set; } = null!;

    public virtual City? IdCityNavigation { get; set; }

    public virtual Gender IdGenderNavigation { get; set; } = null!;

    public virtual User? IdOwnerNavigation { get; set; }

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual ICollection<Movement> Movements { get; set; } = new List<Movement>();
}
