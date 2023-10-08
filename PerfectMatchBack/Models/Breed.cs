using System;
using System.Collections.Generic;

namespace PerfectMatchBack.Models;

public partial class Breed
{
    public int IdBreed { get; set; }

    public string BreedName { get; set; }

    public int IdAnimalType { get; set; }

    public virtual AnimalType IdAnimalTypeNavigation { get; set; }

    public virtual ICollection<Publication> Publications { get; set; } = new List<Publication>();
}
