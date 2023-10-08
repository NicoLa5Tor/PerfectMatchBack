using System;
using System.Collections.Generic;

namespace PerfectMatchBack.Models;

public partial class AnimalType
{
    public int IdAnimalType { get; set; }

    public string AnimalTypeName { get; set; }

    public virtual ICollection<Breed> Breeds { get; set; } = new List<Breed>();

    public virtual ICollection<Publication> Publications { get; set; } = new List<Publication>();
}
